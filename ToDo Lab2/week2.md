Currently we have these requirements (non functional and functional?? Check if we really have any functional requirements. If not, add new.)

- Users must be able to view ther high scores
  - Difficulty - low. 
  - Value to lab2: low.
  - Changes - this requirement will require changes mostly to frontend and some changes to backed. Data will have to be processed, only specific window of dates will be sent to client to preserve bandwith.

- Private games must be implemented
  - Difficulty - high.
  - Value to lab2: high.
  - Changes - user will be able to choose if he wants to play public game or private game, if he chooses private game, the server will generate a specific code and send it to the client. The client will be able to share the code with his friends who will input the code in a seperate page and then get redirected to the shared game page. When the creator of the game will start the game, a counter will count down and then the game will start. After the end of the game a leaderboard could be shown (this could incorperate the current leaderboard element)

- A game history must be implemented
  - Difficulty - low.
  - Value to lab2: low.
  - Changes - simply add a history page for user. Each time a user plays a game, the game id is sent to the server and stored in history table. When user opens the history page, the last n count of records get sent to user.

- A rating system for user-created games must be implemented
  - Difficulty - low.
  - Value to lab2: low.
  - Changes - simply add a rating for each game. Each user can rate a game from 0 to 5 starts, the info gets sent to server and averaged out.


- Users must be able to view the top 10 players for each game
  - Difficulty - low.
  - Value to lab2: low.
  - Changes - send request to server for 10 best players for a specific game, receive the data and display it near the game.

