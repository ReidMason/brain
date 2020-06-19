import json
import os
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


@app.route('/api/note/<note_name>', methods = ['GET', 'POST'])
def get_note_data(note_name):
    if request.method == 'GET':
        with open(os.path.join(DATA_PATH, note_name), 'r') as f:
            return jsonify(f.read())

    elif request.method == 'POST':
        with open(os.path.join(DATA_PATH, note_name), 'w') as f:
            f.write(request.json.get('data'))
        return jsonify({})


if __name__ == '__main__':
    app.run(port = 5000, debug = True)
