import { GameType } from "../components/GameType/GameType";

export interface ButtonProps {
  label: string;
  variant: string;
  onClick?: () => void;
}

export interface Game {
  gameId: number;
  gameDescription: GameDescription;
  gameDifficulty: string;
  creatorId: string;
}

export interface GameConfig {
  gameId: string;
  name: string;
  description: string;
  difficultyLevel: string;
  targetSpeed: string;
  maxTargets: string;
  gameDuration: string;
  gameType: number;
  allowedUsers: string[];
  creatorId: string;
  visibility: number;
}

export interface GameCardProps {
  game: Game;
}

export interface Score {
  userId: number;
  userName: string;
  userEmail: string;
  score: number;
  dateAchieved: string;
  gameType: string;
}

export interface GameDescription {
  gameName: string;
  gameDescr: string;
  gameType: GameType;
}

export interface User {
  id: number;
  name: string;
}

export interface Room {
  Id: string;
  Name: string;
  CreatorId: number;
  Players: string[];
  RoomStatus: string;
}

export interface AvailableRooms {
  Rooms: Room[]
}

export interface RoomTarget {
  X: number;
  Y: number;
}

export interface RoomPlayer {
  Name: string;
  Id: number;
  ReactionTime: number;
}

export interface RoomRoundResults {
  RemainingPlayers: RoomPlayer[];
  EliminatedPlayers: RoomPlayer[];
}