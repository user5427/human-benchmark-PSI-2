import React from "react";

import styles from "./GameCard.module.css";

import { GameCardProps } from "../../types/props";

import GameImage from "../../assets/descr.png";
import { useNavigate } from "react-router-dom";
import { GameType } from "../GameType/GameType";
import { useAuth } from "../../contexts/AuthContext";

const GameCard: React.FC<GameCardProps> = ({ game }) => {
  const navigate = useNavigate();
  const { userId } = useAuth();

  const handleNavigate = () => {
    console.log("Game object:", game); // Debugging line
    switch (game.gameDescription.gameType) {
      case GameType.ReflexTest:
        navigate(
          `/reflex-test?gameId=${game.gameId}&difficulty=${game.gameDifficulty}`
        );
        break;
      case GameType.ReactionTimeChallenge:
        navigate(`/reaction-test?gameId=${game.gameId}`);
        break;
      default:
        break;
    }
  };

  const handleEdit = () => {
    navigate("/game-config", { state: { gameId: game.gameId } });
  };

  return (
    <div className={styles.card}>
      <img
        src={GameImage}
        alt={game.gameDescription.gameName}
        className={styles.image}
        onClick={handleNavigate}
      />
      <h3>{game.gameDescription.gameName}</h3>
      <div>{game.gameDescription.gameDescr}</div>
      {userId === game.creatorId.toString() ? (
        <button className={styles.editButton} onClick={handleEdit}>
          edit
        </button>
      ) : undefined}
    </div>
  );
};

export default GameCard;
