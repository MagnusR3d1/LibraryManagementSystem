SELECT 
    b.id AS book_id, 
    b.book_title, 
    b.status AS book_status,
    i.status AS issue_status 
FROM books b
LEFT JOIN issues i ON b.book_title = i.book_title;