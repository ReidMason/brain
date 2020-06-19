import json
import os
from pprint import pprint

from flask import Flask, jsonify, request
from flask_cors import CORS
from models.Notes import Notes

DATA_PATH = "../data"

app = Flask(__name__)
CORS(app)

_notes = Notes(os.path.join(DATA_PATH, 'notes'))


@app.route('/api/notes')
def notes():
    """ Returns a list of all notes names with additional information

    :return: Object containing the notes names and other information
    """
    _notes.load_notes()
    return jsonify(_notes.get_notes_json())


@app.route('/api/settings', methods = ['GET', 'POST'])
def settings():
    if request.method == 'GET':
        with open(os.path.join(DATA_PATH, 'settings.json'), 'r') as f:
            return jsonify(json.load(f))

    elif request.method == 'POST':
        with open(os.path.join(DATA_PATH, 'settings.json'), 'w') as f:
            f.write(request.json.get('data'))

        return jsonify({})


@app.route('/api/note/<note_name>')
def note_data(note_name: str):
    """ Get the content and information about a specific note.

    :param note_name: The id of the note you want to get
    :return: Information and content of the requested note
    """
    note = _notes.get_note(note_name)
    return jsonify(note.to_json(include_contents = True) if note is not None else note)


@app.route('/api/note', methods = ['POST'])
def save_note_data():
    if (note_content := request.json) is None:
        return "No note content provided", 400
    elif note_content.get('note') is None:
        return "No note object provided", 400

    _notes.save_note(note_content.get('target'), note_content.get('note'))
    return jsonify(_notes.get_notes_json())


if __name__ == '__main__':
    app.run(port = 5000, debug = True)
