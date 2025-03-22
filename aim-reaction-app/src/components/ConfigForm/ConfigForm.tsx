/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useEffect, useState } from "react";

import styles from "./ConfigForm.module.css";
import Button from "../Button/Button";
import { GameType } from "../GameType/GameType";
import { useAuth } from "../../contexts/AuthContext";
import { useLocation, useNavigate } from "react-router";
import { GameConfig } from "../../types/props";

const ConfigForm = () => {
  const location = useLocation();
  const { gameId } = location.state || {};
  const [gameConfig, setGameConfig] = useState<GameConfig | null>(null);
  const [name, setName] = useState<string>("");
  const [descr, setDescr] = useState<string>("");
  const [duration, setDuration] = useState<string>("");
  const [targets, setTargets] = useState<string>("");
  const [difficulty, setDifficulty] = useState<string>("");
  const [speed, setSpeed] = useState<string>("");
  const [type, setType] = useState<number>(0);
  const [visibility, setVisibility] = useState<number>(0);
  const [allowedUsers, setAllowedUsers] = useState<number[]>([]);
  const [availableUsers, setAvailableUsers] = useState<any[]>([]);
  const { userId } = useAuth();
  const navigate = useNavigate();
  const apiUrl = import.meta.env.VITE_API_URL;
  console.log(allowedUsers);
  useEffect(() => {
    const fetchGame = async () => {
      if (!gameId) return;
      try {
        const response = await fetch(
          `${apiUrl}/genericgame/games/${gameId}?userId=${userId}`
        );
        const game = await response.json();
        setGameConfig(game);
      } catch (error) {
        console.error("Error fetching game: ", error);
      }
    };

    fetchGame();
  }, [gameId, apiUrl, userId]);

  useEffect(() => {
    if (gameConfig) {
      setName(gameConfig.name);
      setDescr(gameConfig.description);
      setDuration(gameConfig.gameDuration.toString());
      setTargets(gameConfig.maxTargets.toString());
      setDifficulty(gameConfig.difficultyLevel.toString());
      setSpeed(gameConfig.targetSpeed.toString());
      setType(gameConfig.gameType);
      setVisibility(gameConfig.visibility || 1);
      setAllowedUsers(gameConfig.allowedUsers || []);
    }
  }, [gameConfig]);

  // Fetch available users once on mount
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

  const gameTypeMap: Record<string, GameType> = {
    MovingTargets: GameType.MovingTargets,
    ReflexTest: GameType.ReflexTest,
    CustomChallenge: GameType.CustomChallenge,
    ReactionTest: GameType.ReactionTimeChallenge,
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const gameConfigDto = {
      gameId: gameConfig?.gameId ? parseInt(gameConfig.gameId) : undefined,
      name: name,
      description: descr,
      difficultyLevel: difficulty,
      targetSpeed: parseInt(speed),
      maxTargets: parseInt(targets),
      gameDuration: parseInt(duration),
      gameType: gameTypeMap[type] || 2,
      allowedUsers,
      creatorId: userId,
      visibility,
    };
    try {
      const response = await fetch(`${apiUrl}/GameConfig`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(gameConfigDto),
      });

      if (response.ok) {
        alert("Successfully completed");
        navigate("/games");
      } else {
        const errorData = await response.json();
        alert("Error creating game configuration: " + errorData.message);
      }
    } catch (error) {
      if (error instanceof Error) {
        alert("An error occurred: " + error.message);
      } else {
        alert("An unexpected error occurred.");
      }
    }
  };

  const handleUserSelection = (event: any) => {
    const userId = parseInt(event.target.value);
    if (!userId) return;
    setAllowedUsers((prev) =>
      prev.includes(userId)
        ? prev.filter((id) => id !== userId)
        : [...prev, userId]
    );
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <p>Please specify required configuration to create a game.</p>

      <div className={styles.formContent}>
        <div className={styles.inputItem}>
          <label htmlFor="name">Game Name</label>
          <input
            type="text"
            id="gameNameName"
            placeholder="Enter a name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            className={styles.input}
            minLength={2}
            maxLength={50}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="type">Game type</label>
          <select
            id="type"
            value={type}
            onChange={(e) => setType(parseInt(e.target.value))}
            className={styles.input}
            required
          >
            <option value="">Choose a game type</option>
            <option value="1">ReflexTest</option>
            <option value="2">ReactionTest</option>
          </select>
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="descr">Description</label>
          <textarea
            id="descr"
            placeholder="Enter a description"
            value={descr}
            onChange={(e) => setDescr(e.target.value)}
            className={styles.input}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="duration">Duration (s)</label>
          <input
            type="number"
            id="duration"
            placeholder="Enter a duration"
            value={duration}
            onChange={(e) => setDuration(e.target.value)}
            className={styles.input}
            min={30}
            max={300}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="targets"># of Targets</label>
          <input
            type="number"
            id="targets"
            placeholder="Enter the number of targets"
            value={targets}
            onChange={(e) => setTargets(e.target.value)}
            className={styles.input}
            min={5}
            max={200}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="speed">Speed of Targets</label>
          <input
            type="number"
            id="speed"
            placeholder="Enter the speed of targets"
            value={speed}
            onChange={(e) => setSpeed(e.target.value)}
            className={styles.input}
            min={1}
            max={50}
            required
          />
        </div>

        <div className={styles.inputItem}>
          <label htmlFor="visibility">Visibility</label>
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

        <div className={styles.inputItem}>
          <label htmlFor="difficulty">Difficulty Level</label>
          <select
            id="difficulty"
            value={difficulty}
            onChange={(e) => setDifficulty(e.target.value)}
            className={styles.input}
            required
          >
            <option value="">Choose a difficulty level</option>
            <option value="easy">Easy</option>
            <option value="medium">Medium</option>
            <option value="hard">Hard</option>
          </select>
        </div>
        {visibility === 1 ? (
          <div>
            <label>Allowed Users</label>
            <div className={styles.userList}>
              {availableUsers?.map((user) => (
                <div key={user.id}>
                  <input
                    type="checkbox"
                    value={user.id}
                    checked={allowedUsers.includes(user.id)}
                    onChange={handleUserSelection}
                  />
                  <label>{user.name}</label>
                </div>
              ))}
            </div>
          </div>
        ) : null}
      </div>

      <Button label="Save" variant="secondary" />
    </form>
  );
};

export default ConfigForm;
