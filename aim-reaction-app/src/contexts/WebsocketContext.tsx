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