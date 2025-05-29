
// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/ReflexTest/ReflexTest.tsx ====================

import React, { useState, useEffect } from "react";
import styles from "./ReflexTest.module.css";
import { useAuth } from "../../contexts/AuthContext";
import { useSearchParams, useNavigate } from "react-router-dom";
import hitSound from "../../assets/Target-sound.mp3";
import countSound from "../../assets/CountDown.mp3";
import gameSound from "../../assets/GameStart-Sound.mp3";
import { TargetArea } from "../../components/TargetArea/TargetArea";

const DIFFICULTY_SETTINGS = {
  easy: { spawnInterval: 1200, expiryTime: 1200 },
  medium: { spawnInterval: 800, expiryTime: 800 },
  hard: { spawnInterval: 500, expiryTime: 500 },
};

const ReflexTest: React.FC = () => {
  const { userId } = useAuth();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const gameId = parseInt(searchParams.get("gameId") || "0", 10); // Default to 0 if invalid
  const difficultyParam = searchParams.get("difficulty") || "easy";

  const [difficulty, setDifficulty] = useState<"easy" | "medium" | "hard">("easy");
  const [sessionId, setSessionId] = useState<number | null>(null);
  const [target, setTarget] = useState<number | null>(null);
  const [score, setScore] = useState(0);
  const [missedTargets, setMissedTargets] = useState(0);
  const [gameActive, setGameActive] = useState(false);
  const [expiryTimeout, setExpiryTimeout] = useState<NodeJS.Timeout | null>(null);
  const [countdown, setCountdown] = useState<number | string | null>(null);

  const sanitizedUserId = userId ? parseInt(userId, 10) : null;
  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    if (!gameId || isNaN(gameId)) {
      console.error("Invalid or missing gameId in URL query.");
      navigate("/"); // Redirect to a fallback route
    }

    if (["easy", "medium", "hard"].includes(difficultyParam)) {
      setDifficulty(difficultyParam as "easy" | "medium" | "hard");
    } else {
      console.error("Invalid difficulty level in query parameters.");
    }
  }, [gameId, difficultyParam, navigate]);

  const startGameSession = async () => {
    if (!sanitizedUserId || !gameId) {
      console.error("User ID and gameId are required to start a game session.");
      return;
    }

    try {
      const response = await fetch(
        `${apiUrl}/GenericGame/${sanitizedUserId}/start/1`,
        { method: "POST" }
      );

      if (!response.ok) {
        throw new Error(`Failed to start game session: ${response.statusText}`);
      }

      const session = await response.json();
      setSessionId(session.gameSessionId);
      setGameActive(true);
      console.log("Session ID:", session.gameSessionId);
    } catch (error) {
      console.error("Error starting game session:", error);
    }
  };

  const endGameSession = async () => {
    if (!sessionId) {
      console.error("Session ID is required to end the session.");
      return;
    }

    try {
      const response = await fetch(`${apiUrl}/GenericGame/end/${sessionId}`, {
        method: "POST",
      });

      if (!response.ok) {
        throw new Error(`Failed to end game session: ${response.statusText}`);
      }

      const result = await response.json();
      console.log("Session ended successfully:", result);
    } catch (error) {
      console.error("Error ending game session:", error);
    }
  };

  const saveScore = async (reactionScore: number) => {
    if (!sanitizedUserId || !gameId) {
      console.error("User ID and gameId are required to save the score.");
      return;
    }

    const scoreData = {
      userId: sanitizedUserId,
      value: reactionScore,
      dateAchieved: new Date().toISOString(),
      gameId,
      gameType: 1, // Assuming gameType 1 for Reflex Test
    };

    try {
      const response = await fetch(`${apiUrl}/GenericGame/${sanitizedUserId}/addscore`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(scoreData),
      });

      if (!response.ok) {
        throw new Error(`Error saving score: ${response.statusText}`);
      }

      const data = await response.json();
      console.log("Score saved:", data);
    } catch (error) {
      console.error("Error saving score:", error);
    }
  };

  const handleStartGame = () => {
    setScore(0);
    setMissedTargets(0);
    setCountdown(3);

    let countdownValue = 3;
    const countdownInterval = setInterval(() => {
      if (countdownValue > 1) {
        const countdownSound = new Audio(countSound);
        countdownSound.play();
      }
      countdownValue -= 1;
      if (countdownValue === 0) {
        const startSound = new Audio(gameSound);
        startSound.play();
        setCountdown("Go!");
        setTimeout(() => {
          setCountdown(null);
          startGameSession(); // Start the session when countdown ends
        }, 1000);
        clearInterval(countdownInterval);
      } else {
        setCountdown(countdownValue);
      }
    }, 1000);
  };

  const handleHitTarget = () => {
    const targetHit = new Audio(hitSound);
    targetHit.play();

    setScore((prev) => prev + 1);
    setTarget(null);

    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
      setExpiryTimeout(null);
    }
  };

  const handleStopGame = async () => {
    setGameActive(false);
    if (expiryTimeout) {
      clearTimeout(expiryTimeout);
    }

    try {
      await saveScore(score);
      await endGameSession();
    } catch (error) {
      console.error("Error handling game stop:", error);
    }
  };

  useEffect(() => {
    if (gameActive && target === null && missedTargets < 3) {
      const { spawnInterval, expiryTime } = DIFFICULTY_SETTINGS[difficulty];
      const spawnTimer = setTimeout(() => {
        setTarget(Date.now());
        const expiryTimer = setTimeout(() => {
          setTarget(null);
          setMissedTargets((prev) => prev + 1);
        }, expiryTime);
        setExpiryTimeout(expiryTimer);
      }, spawnInterval);

      return () => {
        clearTimeout(spawnTimer);
        if (expiryTimeout) clearTimeout(expiryTimeout);
      };
    } else if (missedTargets >= 3 && gameActive) {
      handleStopGame();
    }
  }, [gameActive, target, missedTargets, difficulty]);

  return (
    <div className={styles.container}>
      <h2>Reflex Test</h2>
      {!gameActive && countdown === null ? (
        <button onClick={handleStartGame}>Start Game</button>
      ) : (
        <div className={styles.scoreRow}>
          <span className={styles.score}>Score: {score}</span>
          <span className={styles.missedTargets}>Missed: {missedTargets} / 3</span>
          <button onClick={handleStopGame}>Stop Game</button>
        </div>
      )}
        {!!countdown && <div className={styles.countdown}>{countdown}</div>}
        <TargetArea
          showTarget={!countdown && !!sessionId}
          targetX={Math.random()}
          targetY={Math.random()}
          hitTarget={handleHitTarget} />
    </div>
  );
};

export default ReflexTest;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/Login/Login.tsx ====================

import AuthForm from '../../components/Auth/AuthForm'; 

const Login = () => {
  return (
    <div>
      <AuthForm />
    </div>
  );
};

export default Login;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/Leaderboard/Leaderboard.tsx ====================

﻿import styles from './Leaderboard.module.css'
import LeaderboardText from '../../components/LeaderboardText/LeaderboardText';
import LeaderboardTable from '../../components/LeaderboardTable/LeaderboardTable';


const Leaderboard = () => {
    return (
      <section>
            <div className={styles.leaderboard}>
                <LeaderboardText />
                <LeaderboardTable />
         </div>
      </section>  
    )
}

export default Leaderboard

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/MovingTargets/MovingTargets.tsx ====================

const MovingTarget: React.FC = () => {
    return (
      <div>
            MOVING TARGETS
      </div>
    );
  };

export default MovingTarget

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/CreateGame/CreateGame.tsx ====================

import styles from './CreateGame.module.css'
import ConfigForm from '../../components/ConfigForm/ConfigForm'

const CreateGame = () => {
  return (
    <section className={styles.config}>
        <div className='wrapper'>
            <ConfigForm />
        </div>
    </section>
  )
}

export default CreateGame

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/Games/Games.tsx ====================

import FeaturedGames from '../../components/FeaturedGames/FeaturedGames'

const Games = () => {
  return (
    <FeaturedGames />
  )
}

export default Games

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/Multiplayer/Multiplayer.tsx ====================

import MultiplayerRooms from '../../components/MultiplayerRooms/MultiplayerRooms';
import { useAuth } from '../../contexts/AuthContext';
import { useEffect, useState } from 'react';
import { Room, RoomTarget, RoomRoundResults, AvailableRooms } from '../../types/props';
import { TargetArea } from '../../components/TargetArea/TargetArea';
import styles from './Multiplayer.module.css'
import { useWs } from '../../contexts/WebsocketContext';
import { useGameRoom } from '../../contexts/GameRoomContext';
const Multiplayer = () => {
    const { userId } = useAuth();
    const { sendJsonMessage, lastJsonMessage, readyState } = useWs();
    const { room, setRoom } = useGameRoom();
    const playerId = parseInt(userId ?? "");
    const [started, setStarted] = useState(false);
    const [target, setTarget] = useState<RoomTarget | null>(null);
    const [roundResult, setRoundResults] = useState<RoomRoundResults | null>(null);
    const [roundStartTime, setRoundStartTime] = useState<number | null>(null);
    const [rooms, setRooms] = useState<Room[]>([]);

    useEffect(() => {
        if (!lastJsonMessage)
            return;
        const message = lastJsonMessage as { eventType: string };
        switch (message.eventType) {
            case 'RoomResponse':
                setRoom(lastJsonMessage as Room);
                break;
            case 'TargetResponse':
                setTarget(lastJsonMessage as RoomTarget);
                setRoundStartTime(Date.now())
                setStarted(true);
                setRoundResults(null);
                break;
            case 'AvailableRoomsResponse':
                setRooms((lastJsonMessage as AvailableRooms).Rooms);
                break;
            case 'RoomRoundResultsResponse':
                setRoundResults(lastJsonMessage as RoomRoundResults);
                break;
            default:
                console.error('invalid response type');
        }
      }, [lastJsonMessage]);

    const returnHome = () => {
        window.location.href = "/"
    }

    const joinRoom = (roomId: string) => {
        sendJsonMessage({
            "eventType": "JoinRoomEvent",
            "playerId": playerId,
            "roomId": roomId
        });
    }

    const createRoom = (roomName: string, visibility: number, allowedPlayers: number[] ) => {
        sendJsonMessage({
            "eventType": "CreateRoomEvent",
            "playerId": playerId,
            "roomName": roomName,
            "visibility": visibility,
            "allowedPlayers": allowedPlayers
        });
    }

    const startRoom = () => {
        sendJsonMessage({
        "eventType": "StartRoomEvent",
        "playerId": playerId,
        "roomId": room?.Id
        })
    }

    const hitTarget = () => {
        const reactionTime = Date.now() - roundStartTime!;
        sendJsonMessage({
        "eventType": "HitTargetEvent",
        "playerId": playerId,
        "roomId": room?.Id,
        "reactionTime": reactionTime
        })
        setTarget(null);
    }

    if (readyState != 1) {
        return <div>Loading...</div>
    }

    if (roundResult?.EliminatedPlayers.some(player => player.Id === playerId)) {
        setRoom(null);
        return <div className='lost-message'>
            <div>You have lost!</div>
            <button onClick={returnHome}>Return to Home!</button>
        </div>
    }

    if (roundResult?.RemainingPlayers.length === 1) {
        setRoom(null);
        return <div className='win-message'>
            <div>You have won!</div>
            <button onClick={returnHome}>Return to Home!</button>
        </div>
    }
   
    return (
        (!room ?
            <section>
                <div>
                    <MultiplayerRooms rooms={rooms} joinRoom={joinRoom} createRoom={createRoom} />
                </div>
            </section>
            :
            <div className={styles.container}>
                {
                    room && !started ?
                    <div>
                        <div>State: {room?.RoomStatus}</div>
                        <div>Room: {room?.Name}</div>
                        <div>Players: {room?.Players.length}</div>
                        {playerId === room.CreatorId && <button onClick={startRoom}>Start</button>}
                    </div>
                    :
                    <div>
                        {roundResult && <div style={{ display: "flex", gap: "20px", alignItems: "flex-start" }}>
                            <div>
                                <span>Remaining: </span>
                                <ul className={styles.userList}>
                                    {roundResult?.RemainingPlayers.map(p =>
                                        <li key={p.Id}>
                                            <span>{p.Name}</span>
                                            <span>{p.ReactionTime} ms</span>
                                        </li>
                                    )}
                                </ul>
                            </div> 
                            <div>
                                <span>Eliminated: </span>
                                <ul className={styles.userList}>
                                    {roundResult?.EliminatedPlayers.map(p =>
                                        <li key={p.Id}>
                                            <span>{p.Name}</span>
                                            <span>{p.ReactionTime} ms</span>
                                        </li>
                                    )}
                                </ul>
                            </div>
                        </div>  }  
                        <TargetArea targetX={0.01 * (target?.X ?? 0)}
                            targetY={0.01 * (target?.Y ?? 0)}
                            showTarget={!!target}
                            hitTarget={hitTarget} />
                    </div>
                }
            </div>
        )
    )
}

export default Multiplayer

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/Home/Home.tsx ====================

import FeaturedGames from '../../components/FeaturedGames/FeaturedGames';
import Hero from '../../components/Hero/Hero';

import styles from './Home.module.css'

const Home = () => {
  return (
    <section className={styles.home}>
      <div className={"wrapper"}>
        <Hero />
        <FeaturedGames />
      </div>
    </section>
  )
}

export default Home

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/pages/ReactionTest/ReactionTest.tsx ====================

import React, { useState } from "react";
import styles from "./ReactionTest.module.css";
import StartGame from "../../components/StartGame/StartGame";
import ReactionTestLogic from "../../components/ReactionTest/ReactionTestLogic";
import { useAuth } from "../../contexts/AuthContext";
import { useSearchParams } from "react-router-dom";

const ReactionTest: React.FC = () => {
  const { userId } = useAuth();

  const [searchParams] = useSearchParams();

  const gameId = searchParams.get("gameId");

  const [sessionId, setSessionId] = useState<number | null>(null);
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [showReactionTest, setShowReactionTest] = useState(false);
  const [testStarted, setTestStarted] = useState(false);

  const sanitizedUserId = userId ? parseInt(userId, 10) : null;

  const apiUrl = import.meta.env.VITE_API_URL;

  const startGameSession = async () => {
    if (!userId || !gameId) {
      console.error("User ID is required to start a game session.");
      return;
    }
    console.log("UserID:", userId);
    try {
      const response = await fetch(
        `${apiUrl}/GenericGame/${sanitizedUserId}/start/2`, // 2 = reactiontest
        {
          method: "POST",
        }
      );

      const session = await response.json();
      setSessionId(session.gameSessionId);
      console.log("Session id: ", session.gameSessionId);
      setTestStarted(true);
      setShowReactionTest(true);
      setReactionTime(null);

      // Fetch active session count
      const activeCountResponse = await fetch(`${apiUrl}/GenericGame/active`);
      const activeCountData = await activeCountResponse.json();
      console.log("Active users:", activeCountData.activeSessions);
    } catch (error) {
      console.error("Error starting game session:", error);
    }
  };

  const recordReactionTime = async (
    reactionTime: number
  ) => {
    try {
      const addScoreDto = {
        userId: sanitizedUserId,
        value: reactionTime, // Use the reaction time as the score
        dateAchieved: new Date().toISOString(),
        gameId: gameId, 
        gameType: 2,
      };

      const response = await fetch(
        `${apiUrl}/GenericGame/${sanitizedUserId}/addscore`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(addScoreDto), 
        }
      );

      if (!response.ok) {
        throw new Error(`Error saving score: ${response.statusText}`);
      }

      const data = await response.json();
      console.log("Score saved:", data);
    } catch (error) {
      console.error("Error saving score:", error);
    }
  };

  const goBackToStart = () => {
    endGameSession();
    setShowReactionTest(false);
    setTestStarted(false);
    setReactionTime(null);
    setSessionId(null);
  };

  const handleReactionTestComplete = (reactionTime: number) => {
    setReactionTime(reactionTime);
    setShowReactionTest(false);

    if (sessionId) {
      // Save score when the reaction test is complete
      recordReactionTime(reactionTime);
      endGameSession();
    }
  };

  const handleRestart = () => {
    if (userId) {
      startGameSession(); // Directly start the game session with the user ID
    }
  };

  const endGameSession = async () => {
    if (!sessionId) {
      console.error("Session ID is required to end the session.");
      return;
    }
    try {
      console.log("end", sessionId);
      const response = await fetch(
        `${apiUrl}/GenericGame/end/${sessionId}`,
        {
          method: "POST",
        }
      );
      if (response.ok) {
        console.log("Game session ended successfully.");
      } else {
        console.error("Failed to end game session.");
      }
    } catch (error) {
      console.error("Error ending game session:", error);
    }
  };

  return (
    <section className={styles.Reaction}>
      <div className={styles.wrapper}>
        <h2>Reaction Test Game</h2>

        {!testStarted && userId && (
          <StartGame startGameSession={startGameSession} />
        )}
        {showReactionTest && (
          <ReactionTestLogic
            onTestComplete={handleReactionTestComplete}
            sessionId={sessionId}
            goBackToStart={goBackToStart}
          />
        )}
        {reactionTime !== null && (
          <div className={styles.resultBox} onClick={handleRestart}>
            <div className={styles.reactionTimeText}>
              Your reaction time: {reactionTime} ms
            </div>
            <div className={styles.restartText}>
              Click to restart
            </div>
          </div>
        )}
      </div>
    </section>
  );
};

export default ReactionTest;



// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/contexts/AuthContext.tsx ====================

import React, { createContext, useContext, useState, ReactNode, useEffect } from "react";

interface AuthContextType {
  isAuthenticated: boolean;
  userId: string | null;
  login: (userId: string) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [userId, setUserId] = useState<string | null>(null);

  useEffect(() => {
    const storedUserId = sessionStorage.getItem("userId");
    if (storedUserId) {
      setIsAuthenticated(true);
      setUserId(storedUserId);
    }
  }, []);

  const login = (userId: string) => {
    setIsAuthenticated(true);
    setUserId(userId);
    sessionStorage.setItem("userId", userId);
  };
  
  const logout = () => {
    setIsAuthenticated(false);
    setUserId(null);
    sessionStorage.removeItem("userId");
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, userId, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/contexts/WebsocketContext.tsx ====================

/* eslint-disable react-refresh/only-export-components */
// WebSocketProvider.tsx
import { createContext, useContext } from 'react';
import useWebSocket, { ReadyState } from 'react-use-websocket';
import { useAuth } from './AuthContext';
import { SendJsonMessage } from 'react-use-websocket/dist/lib/types';

interface WebSocketContextType {
    sendJsonMessage: SendJsonMessage
    lastJsonMessage: unknown,
    readyState: ReadyState;
}

const WebSocketContext = createContext<WebSocketContextType | null>(null);

export const WebSocketProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const { userId } = useAuth();
    const { sendJsonMessage, lastJsonMessage, readyState } = useWebSocket(
        `${import.meta.env.VITE_API_WS}/${userId}`,
        { reconnectInterval: 3000 }
    );

    return (
        <WebSocketContext.Provider value={{ sendJsonMessage, lastJsonMessage, readyState }}>
            {children}
        </WebSocketContext.Provider>
    );
};

export const useWs = (): WebSocketContextType => {
  const context = useContext(WebSocketContext);
  if (!context) throw new Error('useWs must be used within a WebSocketProvider');
  return context;
};

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/contexts/GameRoomContext.tsx ====================

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


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/StartGame/StartGame.tsx ====================

import React from "react";
import styles from "./startGame.module.css"

const StartGame: React.FC<any> = ({ startGameSession }) => {
  return (
    <div
      className={styles.startBox}
      onClick={startGameSession}
    >
      Press to Start
    </div>
  );
};

export default StartGame;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/LeaderboardTable/LeaderboardTable.tsx ====================

import { useState } from "react";
import styles from "./LeaderboardTable.module.css";
import { Score } from "../../types/props";
import Button from "../Button/Button";
import { GameType } from "../GameType/GameType";

const LeaderboardTable = () => {
    const [scores, setScores] = useState<Score[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [gameType, setGameType] = useState<GameType | null>(null); // Start with no gameType selected

    const apiUrl = import.meta.env.VITE_API_URL;
    const topCount = 100;

    // Fetch leaderboard scores based on gameType
    const fetchScores = async (selectedGameType: GameType) => {
        try {
            setLoading(true);
            setError("");
            setScores([]);

            const response = await fetch(
                `${apiUrl}/Leaderboard/top-scores/${topCount}?gameType=${selectedGameType}`
            );

            if (!response.ok) {
                throw new Error("Network response was not ok");
            }

            const data = await response.json();
            setScores(data);
        } catch (error) {
            console.error("Error fetching scores:", error);
            setError("Failed to load the scores. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    const handleGameTypeChange = (selectedGameType: GameType) => {
        if (selectedGameType !== gameType) {
            setGameType(selectedGameType);
            fetchScores(selectedGameType); // Fetch scores only after selecting a game type
        }
    };

    const sortedScores = scores.sort((a, b) => {
        if (gameType === GameType.ReactionTimeChallenge) {
            return a.score - b.score;
        } else {
            
            return b.score - a.score; 
        }
    });

    return (
        <div>
            <div className={styles.LeaderboardWrapper}>
                <div className={styles.buttonContainer}>
                    <Button
                        label="Reflex Test"
                        variant={gameType === GameType.ReflexTest ? "third" : "primary"}
                        onClick={() => handleGameTypeChange(GameType.ReflexTest)}
                    />
                    <Button
                        label="Reaction Time"
                        variant={
                            gameType === GameType.ReactionTimeChallenge ? "third" : "primary"
                        }
                        onClick={() => handleGameTypeChange(GameType.ReactionTimeChallenge)}
                    />
                </div>

                {!gameType && <p>Please select a game type to view the leaderboard.</p>}

                {loading && <p>Loading scores...</p>}
                {error && <p>{error}</p>}
                {!loading && !error && scores.length === 0 && gameType && (
                    <p>No scores are available for the selected game type.</p>
                )}
                {!loading && !error && scores.length > 0 && (
                    <div className={styles.LeaderboardTableContainer}>
                        <table className={styles.LeaderboardTable}>
                            <thead>
                                <tr>
                                    <th>Rank</th>
                                    <th>User</th>
                                    <th>Score</th>
                                    <th>Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                {sortedScores.map((score, index) => (
                                    <tr key={`${score.userId}-${score.dateAchieved}`}>
                                        <td>{index + 1}</td>
                                        <td>{score.userName}</td>
                                        <td>{score.score}</td>
                                        <td>
                                            {new Date(score.dateAchieved).toLocaleDateString(
                                                "en-CA"
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default LeaderboardTable;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/Button/Button.tsx ====================

import React from 'react';
import { ButtonProps } from '../../types/props';
import styles from './Button.module.css';

const Button: React.FC<ButtonProps> = ({ label, variant = "primary", onClick }) => {
    const getVariantClass = (variant: string) => {
        switch (variant) {
            case "primary":
                return styles.primary;
            case "secondary":
                return styles.secondary;
            case "third":
                return styles.third;
            case "fourth":
                return styles.fourth;
            default:
                return styles.primary; 
        }
    };

    return (
        <button className={`${styles.button} ${getVariantClass(variant)}`} onClick={onClick}>
            {label}
        </button>
    );
}

export default Button;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/Navbar/Navbar.tsx ====================

import { useState, useEffect, useRef } from 'react';
import styles from './Navbar.module.css';
import { Link, useNavigate } from 'react-router-dom';

import logo from '../../assets/logo.svg';
import burgerIcon from '../../assets/burger-icon.svg';
import Button from '../Button/Button';
import { useAuth } from '../../contexts/AuthContext';

const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const { logout } = useAuth();
  const navigate = useNavigate();
  
  const mobileMenuRef = useRef<HTMLDivElement | null>(null); // Reference to the mobile menu
  const burgerIconRef = useRef<HTMLImageElement | null>(null); // Reference to the burger icon

  const toggleMenu = () => {
      setIsMenuOpen(!isMenuOpen);

  };

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  // Close the menu if clicking outside
  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (mobileMenuRef.current && !mobileMenuRef.current.contains(event.target as Node) &&
          burgerIconRef.current && !burgerIconRef.current.contains(event.target as Node)) {
        setIsMenuOpen(false); // Close the menu when clicked outside
      }
    };

    // Attach the event listener for clicking outside
    document.addEventListener('mousedown', handleClickOutside);

    // Cleanup the event listener when the component unmounts
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, [mobileMenuRef]);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth > 1000) {
        setIsMenuOpen(false); 
      }
    };

    window.addEventListener('resize', handleResize);
    handleResize();
    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, []);


  return (
    <nav className={styles.navbar}>
      <Link to={"/"} className={styles.logo}>
        <img src={logo} alt='logo' className={styles.logoIcon} />
        <span>Human Benchmark</span>
      </Link>

      {/* Burger Icon for Mobile */}
      <img
        src={burgerIcon}
        alt="Menu"
        className={styles.burgerIcon}
        onClick={toggleMenu}
        ref={burgerIconRef} 
      />

      {/* Navigation Links for Desktop */}
      <ul className={styles.navbarLinks}>
        <li className={styles.navbarItem}>
          <Link to="/games" className={styles.navbarLink}>Games</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/leaderboards" className={styles.navbarLink}>Leaderboards</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/game-config" className={styles.navbarLink}>Create Game</Link>
        </li>
        <li className={styles.navbarItem}>
          <Link to="/multiplayer" className={styles.navbarLink}>Multiplayer</Link>
        </li>
    
        <Button label={"Logout"} variant='primary' onClick={handleLogout} />
      </ul>

      {/* Mobile Navigation Box */}
      {isMenuOpen && (
        <div ref={mobileMenuRef} className={styles.mobileMenu}>
          <ul className={styles.mobileLinks}>
            <li>
              <Link to="/games" onClick={toggleMenu}>Games</Link>
            </li>
            <li>
              <Link to="/leaderboards" onClick={toggleMenu}>Leaderboards</Link>
            </li>
            <li>
              <Link to="/game-config" onClick={toggleMenu}>Create Game</Link>
            </li>
            <li>
              <Link to="/multiplayer">Multiplayer</Link>
            </li>
            <Button label={"Logout"} variant='primary' onClick={handleLogout}/>         
          </ul>
        </div>
      )}
    </nav>
  );
};

export default Navbar;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/MultiplayerRooms/MultiplayerRooms.tsx ====================

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


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/TargetArea/TargetArea.tsx ====================

import styles from './TargetArea.module.css'

type TargetAreaProps = {
    showTarget: boolean;
    targetX?: number;
    targetY?: number;
    hitTarget: () => void;
}

export const TargetArea = ({showTarget, targetY, targetX, hitTarget}: TargetAreaProps) => {
    if (!showTarget) return;
    return (<div className={styles.targetArea}>
        {<div
            className={styles.target}
            style={{
              top: `${targetY! * 80}%`,
              left: `${targetX! * 80}%`,
            }}
            onClick={hitTarget}
          />}
      </div>)
}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/FeaturedGames/FeaturedGames.tsx ====================

import { useEffect, useState } from "react";

import styles from "./FeaturedGames.module.css";
import GameCard from "../GameCard/GameCard";

import { Game } from "../../types/props";
import { useAuth } from "../../contexts/AuthContext";

const FeaturedGames = () => {
  const { userId } = useAuth();
  const [games, setGames] = useState<Game[]>([]);
  const [activeUserCount, setActiveUserCount] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const apiUrl = import.meta.env.VITE_API_URL;
  useEffect(() => {
    const fetchGames = async () => {
      try {
        setLoading(true);
        setError("");
        const response = await fetch(
          `${apiUrl}/GenericGame/games?userId=${userId}`
        );

        if (!response.ok) {
          throw new Error(`Network response was not ok`);
        }

        const data = await response.json();
        setGames(data);
      } catch {
        setError("Failed to load games.");
      } finally {
        setLoading(false);
      }
    };

    fetchGames();
  }, []);

  useEffect(() => {
    const fetchActiveUserCount = async () => {
      try {
        const response = await fetch(`${apiUrl}/GenericGame/active`, {
          method: "GET",
        });

        if (!response.ok) {
          throw new Error("Failed to fetch active user count.");
        }

        const data = await response.json();
        setActiveUserCount(data.activeSessions);
      } catch (error) {
        console.error("Error fetching active user count:", error);
      }
    };

    fetchActiveUserCount();
  }, []);

  return (
    <section className={styles.games}>
      <div className={styles.available}>
        <h2 className={styles.featuredTitle}>Featured Games</h2>
        {activeUserCount !== null && (
          <span className={styles.activeUsers}>
            Active users: {activeUserCount}
          </span>
        )}

        {/* Conditionally render based on the state */}
        {loading && <p>Loading games...</p>}
        {error && <p>{error}</p>}
        {!loading && !error && games.length === 0 && <p>No games for now :(</p>}
        {!loading && !error && games.length > 0 && (
          <div className={styles.gamesGrid}>
            {games.map((game) => (
              <GameCard key={game.gameId} game={game} />
            ))}
          </div>
        )}
      </div>
    </section>
  );
};

export default FeaturedGames;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/GameCard/GameCard.tsx ====================

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


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/Chat/Chat.tsx ====================

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
  const messagesEndRef = useRef<HTMLDivElement | null>(null);
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
  }, [openChat, globalMessages, roomMessages]);

  useEffect(() => {
    if (!lastJsonMessage) return;

    const message = lastJsonMessage as { eventType: string };
    console.log(`received ${JSON.stringify(message)}`);
    switch (message.eventType) {
      case "GameRoomMessageResponse": {
        setRoomMessages((msgs) => [...msgs, lastJsonMessage as Message]);
        break;
      }
      case "GlobalMessageResponse": {
        console.log("sd");
        const x = lastJsonMessage as Message;
        console.log(x);
        setGlobalMessages((msgs) => [...msgs, x]);
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
            Global
          </button>
          {isInRoom && (
            <button
              className={styles.toggleButton}
              onClick={() => setOpenChat("Room")}
              aria-label="Open room chat"
            >
              Room
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
                      {new Date(createdAt).toLocaleString("default", {
                        day: "2-digit",
                        month: "2-digit",
                        hour: "2-digit",
                        minute: "2-digit",
                        hour12: false, 
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


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/Auth/AuthForm.tsx ====================

import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../Button/Button";
import styles from "./AuthStyles.module.css";
import { useAuth } from "../../contexts/AuthContext"; 

const AuthForm: React.FC = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [isLogin, setIsLogin] = useState(true);

  const navigate = useNavigate();
  const { login, isAuthenticated } = useAuth();

  // Redirect user if already authenticated
  useEffect(() => {
    if (isAuthenticated) {
      navigate("/"); // Redirect to home if already logged in
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const apiUrl = import.meta.env.VITE_API_URL;

    if (isLogin) {
      try {
        const response = await fetch(`${apiUrl}/Auth/login`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email, password }),
        });

        if (!response.ok) throw new Error("Login failed");

        const data = await response.json();

        login(data);
        navigate("/");
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          // alert("Login failed: " + error.message);
        }
      }
    } else {
      try {
        const response = await fetch(`${apiUrl}/Auth/register`, {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ email, password, name }),
        });

        if (!response.ok) throw new Error("Registration failed");

        alert("Registration successful! Please log in.");
        setIsLogin(true); // Switch to login mode after successful registration
      } catch (error) {
        if (error instanceof Error) {
          console.error(error);
          // alert("Registration failed: " + error.message);
        }
      }
    }
  };

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <h2 className={styles.h2}>{isLogin ? "Login" : "Register"}</h2>

      {!isLogin && (
        <div>
          <input
            className={styles.input}
            type="text"
            placeholder="Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
      )}

      <div>
        <input
          className={styles.input}
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>

      <div>
        <input
          className={styles.input}
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>

      <Button label={isLogin ? "Login" : "Register"} variant="secondary" />

      <p className={styles.switchText} onClick={() => setIsLogin(!isLogin)}>
        {isLogin ? "Don't have an account? Register" : "Already have an account? Sign in"}
      </p>
    </form>
  );
};

export default AuthForm;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/ConfigForm/ConfigForm.tsx ====================

/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useEffect, useState } from "react";

import styles from "./ConfigForm.module.css";
import Button from "../Button/Button";
import { useAuth } from "../../contexts/AuthContext";
import { useLocation, useNavigate } from "react-router";
import { GameConfig, User } from "../../types/props";
import { AllowedUserList } from "./AllowedUserList";

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
  const [availableUsers, setAvailableUsers] = useState<User[]>([]);
  const { userId } = useAuth();
  const navigate = useNavigate();
  const apiUrl = import.meta.env.VITE_API_URL;
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
      setVisibility(gameConfig.visibility ?? 0);
      setAllowedUsers((gameConfig.allowedUsers ?? []).map(Number));
    }
  }, [gameConfig]);

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
      gameType: type ?? 2,
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

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <p>Please specify required game configuration.</p>

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
          <AllowedUserList
            allowedUsers={allowedUsers}
            setAllowedUsers={setAllowedUsers}
            availableUsers={availableUsers} />
        ) : null}
      </div>

      <Button label="Save" variant="secondary" />
    </form>
  );
};

export default ConfigForm;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/ConfigForm/AllowedUserList.tsx ====================

import { User } from "../../types/props";
import styles from "./ConfigForm.module.css";

type AllowedUserListProps = {
    allowedUsers: number[],
    setAllowedUsers: (users: number[] | ((prev: number[]) => number[])) => void,
    availableUsers: User[]
}

export const AllowedUserList = ({allowedUsers, setAllowedUsers, availableUsers}: AllowedUserListProps) => {
    
    const handleUserSelection = (event: { target: { value: string; }; }) => {
        const userId = parseInt(event.target.value);
        if (!userId) return;
        setAllowedUsers((prev: number[]) =>
          prev.includes(userId)
            ? prev.filter((id) => id !== userId)
            : [...prev, userId]
        );
      };
    
    return ( <div>
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
)}

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/PrivateRoute/PrivateRoute.tsx ====================

import React from "react";
import { useAuth } from "../../contexts/AuthContext";
import { Navigate } from "react-router-dom";

interface PrivateRouteProps {
  element: React.ComponentType<any>;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ element: Component }) => {
  const { isAuthenticated } = useAuth(); // Use authentication context

  // If the user is not authenticated, redirect to the register page
  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }

  // Render the protected component if authenticated
  return <Component />;
};

export default PrivateRoute;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/LeaderboardText/LeaderboardText.tsx ====================

import styles from './LeaderboardText.module.css'

const LeaderboardText = () => {
    return (
        <section>
            <div className={styles.LeaderboardText}>
            <h1>Leaderboard</h1>
            <p>Top users</p>
          </div>
        </section>
    )
}

export default LeaderboardText

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/Hero/Hero.tsx ====================

import styles from './Hero.module.css'

const Hero = () => {
  return (
    <section className={styles.hero}>
      <div className={styles.overlay}></div>
      <div className={styles.content}>
        <h1>Train your aim</h1>
        <p>Improve your accuracy and reaction time with aim training games</p>

      </div>
    </section>
  )
}

export default Hero

// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/components/ReactionTest/ReactionTestLogic.tsx ====================

// ReactionTestLogic.tsx
import React, { useEffect, useState } from "react";
import styles from "./ReactionTestLogic.module.css"; // Import CSS styles

const ReactionTestLogic: React.FC<any> = ({ onTestComplete, sessionId, goBackToStart }) => {
  const [waitingForClick, setWaitingForClick] = useState(false);
  const [startTime, setStartTime] = useState<number | null>(null);
  // @ts-ignore
  const [reactionTime, setReactionTime] = useState<number | null>(null);

  const apiUrl = import.meta.env.VITE_API_URL;

  useEffect(() => {
    const randomDelay = Math.random() * 5000 + 2000; // 2-7s delay

    const timeout = setTimeout(() => {
      setWaitingForClick(true);
      setStartTime(Date.now());
    }, randomDelay);

    return () => clearTimeout(timeout);
  }, []);

  const handleClick = async () => {
    if (!waitingForClick) {
      goBackToStart();
    } else if (startTime !== null) {
      const reactionTime = Date.now() - startTime;
      setReactionTime(reactionTime);
      setWaitingForClick(false);

      if (sessionId) {
          await endGameSession(sessionId);
          onTestComplete(reactionTime);
      }
    }
  };

  const endGameSession = async (sessionId: number) => {
    try {
      const response = await fetch(`${apiUrl}/GenericGame/end/${sessionId}`, {
        method: "POST",
      });

      if (!response.ok) {
        throw new Error("Failed to end the session");
      }

      const result = await response.json();
      console.log("Session ended successfully:", result);
    } catch (error) {
      console.error("Error ending the session:", error);
    }
  };

  return (
    <div
      className={`${styles.testBox} ${
        waitingForClick ? styles.active : styles.inactive
      }`}
      onClick={handleClick}
    >
      {waitingForClick ? "Press Now!" : "Wait..."}
    </div>
  );
};

export default ReactionTestLogic;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/App.tsx ====================

import { BrowserRouter, Route, Routes, useLocation } from "react-router-dom";
import Home from "./pages/Home/Home";
import "./App.css";
import Navbar from "./components/Navbar/Navbar";
import CreateGame from "./pages/CreateGame/CreateGame";
import Leaderboard from "./pages/Leaderboard/Leaderboard";
import ReactionTest from "./pages/ReactionTest/ReactionTest";
import MovingTarget from "./pages/MovingTargets/MovingTargets";
import ReflexTest from "./pages/ReflexTest/ReflexTest";
import PrivateRoute from "./components/PrivateRoute/PrivateRoute";
import Login from "./pages/Login/Login";
import { AuthProvider } from "./contexts/AuthContext";
import Games from "./pages/Games/Games";
import Multiplayer from "./pages/Multiplayer/Multiplayer";
import { WebSocketProvider } from "./contexts/WebsocketContext";
import Chat from "./components/Chat/Chat";
import { GameRoomProvider } from "./contexts/GameRoomContext";

const Layout: React.FC<React.PropsWithChildren<object>> = ({ children }) => {
  const location = useLocation();

  const hideNavbarRoutes = ["/login"];

  const showNavbar = !hideNavbarRoutes.includes(location.pathname);

  return (
    <>
      {showNavbar && <Navbar />}
      {children}
      <Chat/>
    </>
  );
};

function App() {
  return (
    <AuthProvider>
      <WebSocketProvider>
        <GameRoomProvider>
      <BrowserRouter>
        <Layout>
          <Routes>
            <Route path="/" element={<PrivateRoute element={Home} />} />
            <Route
              path="/game-config"
              element={<PrivateRoute element={CreateGame} />}
            />
            <Route
              path="/leaderboards"
              element={<PrivateRoute element={Leaderboard} />}
            />
            <Route
              path="/reaction-test"
              element={<PrivateRoute element={ReactionTest} />}
            />
            <Route
              path="/movingTargets"
              element={<PrivateRoute element={MovingTarget} />}
            />
            <Route
              path="/reflex-test"
              element={<PrivateRoute element={ReflexTest} />}
            />
            <Route path="/games" element={<PrivateRoute element={Games} />} />
            <Route path="/multiplayer" element={<PrivateRoute element={Multiplayer} />} />
            <Route path="/login" element={<Login />} />
          </Routes>
        </Layout>
          </BrowserRouter>
          </GameRoomProvider>
       </WebSocketProvider>
    </AuthProvider>
  );
}

export default App;


// ==================== FILE: /home/user/Documents/PSI/Human-Benchmark/aim-reaction-app/src/main.tsx ====================

import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.tsx'
import './index.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)

