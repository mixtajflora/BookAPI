-- Adatbázis létrehozása
CREATE DATABASE BookAPI;

-- Adatbázis használata
USE BookAPI;

-- Könyvek tábla
CREATE TABLE Books (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    genre VARCHAR(100),
    publication_year INT,
    publisher VARCHAR(255),
    isbn VARCHAR(13) UNIQUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);


INSERT INTO Books (title, author, genre, publication_year, publisher, isbn)
VALUES 
    ('The Catcher in the Rye', 'J.D. Salinger', 'Fiction', 1951, 'Little, Brown and Company', '9780316769488'),
    ('Pride and Prejudice', 'Jane Austen', 'Romance', 1813, 'T. Egerton, Whitehall', '9780141439518'),
    ('Moby-Dick', 'Herman Melville', 'Adventure', 1851, 'Harper & Brothers', '9780142437247'),
    ('War and Peace', 'Leo Tolstoy', 'Historical Fiction', 1869, 'The Russian Messenger', '9781853260629'),
    ('The Hobbit', 'J.R.R. Tolkien', 'Fantasy', 1937, 'George Allen & Unwin', '9780547928227'),
    ('Brave New World', 'Aldous Huxley', 'Dystopian', 1932, 'Chatto & Windus', '9780060850524'),
    ('The Alchemist', 'Paulo Coelho', 'Philosophical Fiction', 1988, 'HarperOne', '9780062315007'),
    ('Crime and Punishment', 'Fyodor Dostoevsky', 'Psychological Fiction', 1866, 'The Russian Messenger', '9780486415871'),
    ('The Divine Comedy', 'Dante Alighieri', 'Epic Poetry', 1320, 'None (Historical)', '9780140448955'),
    ('Hamlet', 'William Shakespeare', 'Tragedy', 1603, 'None (Historical)', '9780140714548');
