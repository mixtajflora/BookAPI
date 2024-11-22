const uri = 'api/books'; // A könyvek API végpontja
let books = [];

// Könyvek lekérése
function getBooks() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayBooks(data))
        .catch(error => console.error('Unable to get books.', error));
}

// Új könyv hozzáadása
function addBook() {
    const titleTextbox = document.getElementById('add-title');
    const authorTextbox = document.getElementById('add-author');
    const genreTextbox = document.getElementById('add-genre');
    const yearTextbox = document.getElementById('add-year');
    const publisherTextbox = document.getElementById('add-publisher');
    const isbnTextbox = document.getElementById('add-isbn');

    const book = {
        title: titleTextbox.value.trim(),
        author: authorTextbox.value.trim(),
        genre: genreTextbox.value.trim(),
        publicationYear: parseInt(yearTextbox.value, 10) || 0,
        publisher: publisherTextbox.value.trim(),
        isbn: isbnTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(book)
    })
        .then(response => response.json())
        .then(() => {
            getBooks();
            // Az input mezők ürítése
            titleTextbox.value = '';
            authorTextbox.value = '';
            genreTextbox.value = '';
            yearTextbox.value = '';
            publisherTextbox.value = '';
            isbnTextbox.value = '';
        })
        .catch(error => console.error('Unable to add book.', error));
}

// Könyv törlése
function deleteBook(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getBooks())
        .catch(error => console.error('Unable to delete book.', error));
}

// Szerkesztési űrlap megjelenítése
function displayEditForm(id) {
    const book = books.find(book => book.id === id);

    document.getElementById('edit-id').value = book.id;
    document.getElementById('edit-title').value = book.title;
    document.getElementById('edit-author').value = book.author;
    document.getElementById('edit-genre').value = book.genre;
    document.getElementById('edit-year').value = book.publicationYear;
    document.getElementById('edit-publisher').value = book.publisher;
    document.getElementById('edit-isbn').value = book.isbn;

    document.getElementById('editForm').style.display = 'block';
}

// Könyv frissítése
function updateBook() {
    // Az edit form ID-jának értékei
    const bookId = document.getElementById('edit-id').value;  // A könyv ID-ja
    const updatedBook = {
        id: parseInt(bookId),  // A könyv ID-ja
        title: document.getElementById('edit-title').value.trim(),  // Cím
        author: document.getElementById('edit-author').value.trim(),  // Szerző
        publicationYear: parseInt(document.getElementById('edit-year').value.trim()),  // Kiadás éve
        genre: document.getElementById('edit-genre').value.trim(),  // Műfaj
        publisher: document.getElementById('edit-publisher').value.trim(),  // Kiadó
        isbn: document.getElementById('edit-isbn').value.trim()  // ISBN
    };

    // PUT kérés küldése a backend API végpontra
    fetch(`${uri}/${bookId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedBook)  // A módosított könyv adatokat küldjük
    })
        .then(response => {
            if (response.ok) {
                // Ha sikeres volt a válasz, újratöltjük a könyvek listáját
                getBooks();
                closeEditForm();  // Bezárjuk az edit formot
            } else {
                console.error('Failed to update the book.');
                alert('Failed to update the book.');
            }
        })
        .catch(error => {
            console.error('Error updating the book:', error);
            alert('Error updating the book. Please try again later.');
        });
}

// Szerkesztési űrlap bezárása
function closeEditForm() {
    document.getElementById('editForm').style.display = 'none';
}

// Könyvek megjelenítése
function _displayBooks(data) {
    const tBody = document.getElementById('books');
    tBody.innerHTML = '';

    data.forEach(book => {
        let editButton = document.createElement('button');
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${book.id})`);

        let deleteButton = document.createElement('button');
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteBook(${book.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let titleNode = document.createTextNode(book.title);
        td1.appendChild(titleNode);

        let td2 = tr.insertCell(1);
        let authorNode = document.createTextNode(book.author);
        td2.appendChild(authorNode);

        let td3 = tr.insertCell(2);
        let genreNode = document.createTextNode(book.genre);
        td3.appendChild(genreNode);

        let td4 = tr.insertCell(3);
        let yearNode = document.createTextNode(book.publicationYear);
        td4.appendChild(yearNode);

        let td5 = tr.insertCell(4);
        let publisherNode = document.createTextNode(book.publisher);
        td5.appendChild(publisherNode);

        let td6 = tr.insertCell(5);
        let isbnNode = document.createTextNode(book.isbn);
        td6.appendChild(isbnNode);

        let td7 = tr.insertCell(6);
        td7.appendChild(editButton);

        let td8 = tr.insertCell(7);
        td8.appendChild(deleteButton);
    });

    books = data;
}
