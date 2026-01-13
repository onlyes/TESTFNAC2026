### Fix Bugs

1- If you run the project and you try to add a new book with the following request:

`[Post] api/book/add {"title" : "Book 6", "author": "Author 6" }`

it will throw an error. Identify the bug and fix it.

------------------------------------------------------------------

2 - if you add a new book with the following request: 

`[Post] api/book/add { "title" : "Book 6", "author": "Author 5" }`

Check the response, is the book added successfully? If not, identify the bug and fix it.


------------------------------------------------------------------

### Implement New Feature


1- Implement this method : Get the Customers who Borrowed a books in `LibraryService`
	and expose this via appropriate API endpoint in LibraryController.

2- Implement unit tests for the this method.


------------------------------------------------------------------