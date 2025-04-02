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