import json
import os
from pprint import pprint

from flask import Flask, jsonify, request, send_file
from flask_cors import CORS

DATA_PATH = "../data"

app = Flask(__name__)
CORS(app)


@app.route('/api/notes')
def index():
    notes = os.listdir(DATA_PATH)
    return jsonify([x for x in notes])


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
    app.run(host = '0.0.0.0', port = 5003, debug = True)
