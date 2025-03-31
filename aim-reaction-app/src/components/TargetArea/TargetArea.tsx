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