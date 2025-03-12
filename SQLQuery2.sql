ALTER TABLE books
ADD image VARCHAR(MAX) NULL

SELECT * FROM books

DELETE FROM books

CREATE TABLE issues
(
  id INT PRIMARY KEY IDENTITY(1,1),
  issue_id VARCHAR(MAX) NULL,
  full_name VARCHAR(MAX) NULL,
  contact VARCHAR(MAX) NULL,
  email VARCHAR(MAX) NULL,
  book_title VARCHAR(MAX) NULL,
  author VARCHAR(MAX) NULL,
  image VARCHAR(MAX) NULL,
  status VARCHAR(MAX) NULL,
  issue_date DATE NULL,
  return_date DATE NULL,
  date_insert DATE NULL,
  date_update DATE NULL,
  date_delete DATE NULL,
)

SELECT * FROM issues

DELETE FROM issues

ALTER TABLE issues
DROP COLUMN image 

SELECT * FROM issues WHERE date_delete IS NULL

SELECT COUNT(id) FROM issues WHERE date_delete IS NULL