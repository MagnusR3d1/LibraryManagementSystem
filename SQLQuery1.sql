CREATE TABLE users
(
  id INT PRIMARY KEY IDENTITY(1,1),
  email VARCHAR(MAX) NULL,
  username VARCHAR(MAX) NULL, 
  password VARCHAR(MAX) NULL,
  date_register DATE NULL
)

SELECT * FROM users