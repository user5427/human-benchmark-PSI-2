import MultiplayerRooms from '../../components/MultiplayerRooms/MultiplayerRooms';
import useWebSocket from 'react-use-websocket';
import { useAuth } from '../../contexts/AuthContext';
import { useEffect, useState } from 'react';
import { Room, RoomTarget, RoomRoundResults, AvailableRooms } from '../../types/props';
import { TargetArea } from '../../components/TargetArea/TargetArea';
import styles from './Multiplayer.module.css'
const Multiplayer = () => {
    const apiWs = import.meta.env.VITE_API_WS;
    const { userId } = useAuth();
    const playerId = parseInt(userId ?? "");
    const [started, setStarted] = useState(false);
    const [room, setRoom] = useState<Room | null>(null);
    const [target, setTarget] = useState<RoomTarget | null>(null);
    const [roundResult, setRoundResults] = useState<RoomRoundResults | null>(null);
    const [roundStartTime, setRoundStartTime] = useState<number | null>(null);
    const [rooms, setRooms] = useState<Room[]>([]);
    const { sendJsonMessage, lastJsonMessage, readyState } = useWebSocket(`${apiWs}/${userId}`, {reconnectInterval: 3000});

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
        return <div className='lost-message'>
            <div>You have lost!</div>
            <button onClick={returnHome}>Return to Home!</button>
        </div>
    }

    if (roundResult?.RemainingPlayers.length === 1) {
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