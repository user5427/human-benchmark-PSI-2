import { useState } from "react";
import { Room } from "../../types/props";
import styles from "./MultiplayerRooms.module.css"

type OnlineRoomsProp = {
  joinRoom: (roomId: string) => void;
  createRoom: (roomName: string) => void;
  rooms: Room[];
}


const OnlineRooms = ({ joinRoom, createRoom, rooms }: OnlineRoomsProp) => {
  console.log(rooms)
  const [showModal, setShowModal] = useState(false);
  const [newRoomName, setNewRoomName] = useState("");

  const handleCreateRoom = (roomName: string) => {
    createRoom(roomName);
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
            <span>{room.Players.length} players</span>
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
            onChange={(e) => setNewRoomName(e.target.value)}
            placeholder="Enter room title"
          />
          <button onClick={() => handleCreateRoom(newRoomName)}>Create</button>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      )}
    </section>
  );
};

export default OnlineRooms;
