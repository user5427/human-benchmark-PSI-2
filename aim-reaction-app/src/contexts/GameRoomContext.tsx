import { createContext, useContext, useState, ReactNode } from "react";
import { Room } from "../types/props";

type GameRoomContextType = {
  room: Room | null;
  setRoom: (room: Room | null) => void;
};

const GameRoomContext = createContext<GameRoomContextType | undefined>(
  undefined
);

export const GameRoomProvider = ({ children }: { children: ReactNode }) => {
  const [room, setRoom] = useState<Room | null>(null);

  return (
    <GameRoomContext.Provider value={{ room, setRoom }}>
      {children}
    </GameRoomContext.Provider>
  );
};

export const useGameRoom = () => {
  const context = useContext(GameRoomContext);
  if (!context) {
    throw new Error("useGameRoom must be used within a GameRoomProvider");
  }
  return context;
};
