import { useState, useRef, useEffect } from "react";
import styles from "./Chat.module.css";
import { useWs } from "../../contexts/WebsocketContext";
import { useAuth } from "../../contexts/AuthContext";
import { useGameRoom } from "../../contexts/GameRoomContext";

type Message = {
  sender: string;
  content: string;
  createdAt: string;
};

type GlobalMessageRequest = {
  eventType: "GlobalMessageRequest";
  senderId: number;
  content: string;
};

type RoomMessageRequest = {
  eventType: "GameRoomMessageRequest";
  gameRoomId: string;
  senderId: number;
  content: string;
};

type ChatScope = "Global" | "Room";

const Chat = () => {
  const { isAuthenticated, userId } = useAuth();
  const { room } = useGameRoom();
  const isInRoom = !!room;
  const [openChat, setOpenChat] = useState<ChatScope | null>(null);
  const { sendJsonMessage, lastJsonMessage, readyState } = useWs();

  const [globalMessages, setGlobalMessages] = useState<Message[]>([
    {
      sender: "GlobalSupport",
      content: "Welcome to global chat!",
      createdAt: new Date().toISOString(),
    },
  ]);
  const [roomMessages, setRoomMessages] = useState<Message[]>([
    {
      sender: "RoomBot",
      content: "Welcome to the room!",
      createdAt: new Date().toISOString(),
    },
  ]);

  const [input, setInput] = useState("");
  const messagesEndRef = useRef(null);
  const apiUrl = import.meta.env.VITE_API_URL;
  useEffect(() => {
    const fetchGlobalMessages = async () => {
      try {
        const response = await fetch(
          `${apiUrl}/message/global?user-id=${userId}`
        );
        const messages: Message[] = await response.json();
        setGlobalMessages(messages);
      } catch (error) {
        console.error("Error fetching global messages:", error);
      }
    };
    if (!userId) return;
    fetchGlobalMessages();
  }, [userId]);

  useEffect(() => {
    const fetchRoomMessages = async () => {
      try {
        const response = await fetch(
          `${apiUrl}/message/room?user-id=${userId}&room-id=${room!.Id}`
        );
        const messages: Message[] = await response.json();
        setRoomMessages(messages);
      } catch (error) {
        console.error("Error fetching room messages:", error);
      }
    };
    if (!userId || !room) return;
    fetchRoomMessages();
  }, [userId, room]);

  useEffect(() => {
    if (openChat && messagesEndRef.current) {
      messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
    }
  }, [globalMessages, roomMessages, openChat]);

  useEffect(() => {
    if (!lastJsonMessage) return;

    const message = lastJsonMessage as { eventType: string };

    switch (message.eventType) {
      case "GameRoomMessageResponse": {
        setRoomMessages((msgs) => [...msgs, lastJsonMessage as Message]);
        break;
      }
      case "GlobalMessageResponse": {
        console.log(message);
        setGlobalMessages((msgs) => [...msgs, lastJsonMessage as Message]);
        break;
      }
      default:
        console.error("Invalid response type:", message.eventType);
    }
  }, [lastJsonMessage]);

  if (!isAuthenticated) return;

  const handleSend = () => {
    const trimmed = input.trim();
    if (!trimmed) return;

    if (openChat === "Global") {
      const message: GlobalMessageRequest = {
        eventType: "GlobalMessageRequest",
        senderId: parseInt(userId!),
        content: trimmed,
      };
      sendJsonMessage(message);
    } else if (openChat === "Room" && room) {
      const message: RoomMessageRequest = {
        eventType: "GameRoomMessageRequest",
        gameRoomId: room.Id,
        senderId: parseInt(userId!),
        content: trimmed,
      };
      sendJsonMessage(message);
    }
    setInput("");
  };

  const onKeyDown = (e: {
    key: string;
    shiftKey: unknown;
    preventDefault: () => void;
  }) => {
    if (e.key === "Enter" && !e.shiftKey) {
      e.preventDefault();
      handleSend();
    }
  };

  const messages = openChat === "Global" ? globalMessages : roomMessages;

  return (
    <div className={styles.container}>
      {!openChat ? (
        <div className={styles.buttonGroup}>
          <button
            className={styles.toggleButton}
            onClick={() => setOpenChat("Global")}
            aria-label="Open global chat"
          >
            🌐 Global
          </button>
          {isInRoom && (
            <button
              className={styles.toggleButton}
              onClick={() => setOpenChat("Room")}
              aria-label="Open room chat"
            >
              🏠 Room
            </button>
          )}
        </div>
      ) : !readyState ? (
        <div>Loading...</div>
      ) : (
        <div className={styles.chatWindow}>
          <div className={styles.header}>
            <span>{openChat === "Global" ? "Global Chat" : "Room Chat"}</span>
            <button
              className={styles.closeButton}
              onClick={() => setOpenChat(null)}
              aria-label="Close chat"
            >
              ×
            </button>
          </div>

          <div className={styles.messagesPanel}>
            {messages.map(
              (
                { sender: sender, content: content, createdAt: createdAt },
                index
              ) => (
                <div key={index} className={styles.messageCard}>
                  <div className={styles.messageHeader}>
                    <span className={styles.sender}>{sender}</span>
                    <span className={styles.date}>
                      {new Date(createdAt).toLocaleTimeString([], {
                        hour: "2-digit",
                        minute: "2-digit",
                      })}
                    </span>
                  </div>
                  <div className={styles.messageText}>{content}</div>
                </div>
              )
            )}

            <div ref={messagesEndRef} />
          </div>

          <div className={styles.inputArea}>
            <textarea
              className={styles.input}
              value={input}
              onChange={(e) => setInput(e.target.value)}
              onKeyDown={onKeyDown}
              placeholder="Type a message..."
              rows={2}
            />
          </div>
        </div>
      )}
    </div>
  );
};

export default Chat;