import { useEffect, useState } from "react";
import { Room, User } from "../../types/props";
import styles from "./MultiplayerRooms.module.css"
import { useAuth } from "../../contexts/AuthContext";
import { AllowedUserList } from "../ConfigForm/AllowedUserList";

type OnlineRoomsProp = {
  joinRoom: (roomId: string) => void;
  createRoom: (roomName: string, visibility: number, allowedPlayers: number[]) => void;
  rooms: Room[];
}


const MultiplayerRooms = ({ joinRoom, createRoom, rooms }: OnlineRoomsProp) => {
  const { userId } = useAuth();
  const [showModal, setShowModal] = useState(false);
  const [newRoomName, setNewRoomName] = useState("");
  const [allowedUsers, setAllowedUsers] = useState<number[]>([]);
  const [availableUsers, setAvailableUsers] = useState<User[]>([]);
  const [visibility, setVisibility] = useState<number>(0);
  
  const apiUrl = import.meta.env.VITE_API_URL;
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const response = await fetch(`${apiUrl}/Users?userId=${userId}`);
        const users = await response.json();
        setAvailableUsers(users);
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    };

    fetchUsers();
  }, [apiUrl, userId]);

  const handleCreateRoom = (roomName: string) => {
    createRoom(roomName, visibility, allowedUsers);
    setNewRoomName("");
    setShowModal(false);
  }

  const handleCancel = () => {
    setShowModal(false);
    setNewRoomName("");
  };
    
  return (
    <section className={styles.section}>
      <div className={styles.header}>
      <h2>Active Online Rooms</h2>
      <button onClick={() => setShowModal(true)}>Create Room</button>
      </div>
      {rooms.length === 0 && <p>No rooms available.</p>}
      <div className={styles.roomList}>
        {rooms.map((room) => (
          <div className={styles.roomItem} key={room.Id}>
            <span>{room.Name}</span>
            <span>{room.Players.length} player</span>
            <button onClick={() => joinRoom(room.Id)}>Join</button>
          </div>
        ))}
      </div>
      {showModal && (
        <div className={styles.modal}>
          <h3>Create New Room</h3>
          <input
            type="text"
            value={newRoomName}
            className={styles.enterRoomInput}
            onChange={(e) => setNewRoomName(e.target.value)}
            placeholder="Enter room title"
          />
          <div className={styles.inputItem}>
            <select
              id="visibility"
              value={visibility}
              onChange={(e) => setVisibility(parseInt(e.target.value))}
              className={styles.input}
              required
            >
              <option value="0">Public</option>
              <option value="1">Private</option>
            </select>
          </div>
          {visibility === 1 ? (
                    <AllowedUserList
                      allowedUsers={allowedUsers}
                      setAllowedUsers={setAllowedUsers}
                      availableUsers={availableUsers} />
                  ) : null}
          <button onClick={() => handleCreateRoom(newRoomName)}>Create</button>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      )}
    </section>
  );
};

export default MultiplayerRooms;
