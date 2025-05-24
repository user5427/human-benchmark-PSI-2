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

const Layout: React.FC<React.PropsWithChildren<object>> = ({ children }) => {
  const location = useLocation();

  const hideNavbarRoutes = ["/login"];

  const showNavbar = !hideNavbarRoutes.includes(location.pathname);

  return (
    <>
      {showNavbar && <Navbar />}
      {children}
    </>
  );
};

function App() {
  return (
    <AuthProvider>
      <WebSocketProvider>
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
        </WebSocketProvider>
    </AuthProvider>
  );
}

export default App;
