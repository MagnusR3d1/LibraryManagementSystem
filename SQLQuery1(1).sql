CREATE TABLE books
(
  id INT PRIMARY KEY IDENTITY(1,1),
  book_title VARCHAR(MAX) NULL,	
  author VARCHAR(MAX) NULL,
  published_date DATE NULL,
  status VARCHAR(MAX) NULL, 
  date_insert DATE NULL,
  date_update DATE NULL,
  date_delete DATE NULL
)

SELECT * FROM books WHERE date_delete IS NULL