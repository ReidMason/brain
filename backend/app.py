import json

from flask import Flask, jsonify, request
from flask_cors import CORS

app = Flask(__name__)
CORS(app)

NOTES_PATH = '../data/notes.json'


@app.route('/api/notes', methods = ['GET', 'POST'])
def notes():
    if request.method == 'GET':
        with open(NOTES_PATH, 'r') as f:
            return json.load(f)

    elif request.method == 'POST':
        with open(NOTES_PATH, 'w') as f:
            json.dump(request.json, f)

        return jsonify({})


if __name__ == '__main__':
    app.run(host = '0.0.0.0', port = 5000, debug = True)
