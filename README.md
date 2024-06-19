# NoteAndChords

NoteAndChords is a console application written in C# that helps musicians and music enthusiasts to create and manage musical notes and chords.

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Examples](#examples)
- [Contributing](#contributing)
- [License](#license)

## Features
- Create and manage musical notes.
- Generate chords from given notes.
- Save and load notes and chords.
- User-friendly command-line interface.

## Installation

1. Clone the repository:
    ```
    git clone https://github.com/kovmak/NoteAndChords.git
    ```

2. Navigate to the project directory:
    ```
    cd NoteAndChords
    ```

3. Build the project using the .NET CLI:
    ```
    dotnet build
    ```

## Usage

To run the application, use the following command:
```sh
dotnet run
```

The application supports the following commands:

- `addnote <note>`: Adds a new note.
- `addchord <chord>`: Adds a new chord.
- `listnotes`: Lists all notes.
- `listchords`: Lists all chords.
- `savenotes <filename>`: Saves notes to a file.
- `savechords <filename>`: Saves chords to a file.
- `loadnotes <filename>`: Loads notes from a file.
- `loadchords <filename>`: Loads chords from a file.

## Examples

1. Adding a note:
    ```sh
    addnote C
    ```

2. Adding a chord:
    ```sh
    addchord Cm
    ```

3. Listing all notes:
    ```sh
    listnotes
    ```

4. Saving notes to a file:
    ```sh
    savenotes notes.txt
    ```

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes.
4. Commit your changes (`git commit -am 'Add new feature'`).
5. Push to the branch (`git push origin feature-branch`).
6. Create a new Pull Request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
