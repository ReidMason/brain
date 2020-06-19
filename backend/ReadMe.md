#Documentation
The api is accessible through the `http://localhost:5000/api/` route.\
When using post routes don't forget to specify the `content-type` as `application/json` in the request headers.

## Setup
[Install python 3.8.2 or higher.](https://www.python.org/downloads/) \
Install requirements
```bash
pip install -r requirements.txt
```
run `app.py`
```bash
python app.py
```

## Endpoints
#### URL: `/api/notes`
#### Method: `GET`
#### Description:
Returns a list of all the notes.\
This contains data for the time the note was created, the time it was last modified and the name of the note.
#### Sample response
Request: `.get("http://localhost:5000/api/notes")`
```json
[
  {
    "created": "2020-06-18T21:46:19Z",
    "modified": "2020-06-19T22:31:11Z",
    "name": "Note 1"
  },
  {
    "created": "2020-06-18T21:33:55Z",
    "modified": "2020-06-19T19:26:34Z",
    "name": "Note 2"
  },
  {
    "created": "2020-06-19T22:48:05Z",
    "modified": "2020-06-20T00:28:45Z",
    "name": "Note 3"
  }
]
```
---
#### URL: `/api/note/<note name>`
#### Method: `GET`
#### Description:
Returns details and contents of the specified note.\
This contains:
- Note name
- Time created
- Time modified
- Note contents
#### Sample response
Request: `.get("http://localhost:5000/api/note/Note 3")`
```json
{
  "contents": "<h1>This is a new note</h1>",
  "created": "2020-06-19T22:48:05Z",
  "modified": "2020-06-19T22:48:05Z",
  "name": "Note 3"
}
```
#### Method: `POST`
#### Description:
Saves the passed note data.\
The `target` property in the payload is the name of the note you want to target with your changes.\
The `note` property contains the new note object you want to save. This can include additional information like
`created` and `modified` as these will just be ignored so feel free to just use a modified note object.\
You will also use this endpoint for creating notes, simply pass a value of `null` into the `target` property and the
note will be created using the note object provided in the `note` property.
### Payload:
```json
{
    "target": "Note name",
    "note": {
        "contents": "<h1>Note contents</h1>",
        "name": "New note name"
    }
}
```
#### Sample response
This returns an updated list of notes.
```json
[
  {
    "created": "2020-06-18T21:46:19Z",
    "modified": "2020-06-19T22:31:11Z",
    "name": "Note 1"
  },
  {
    "created": "2020-06-18T21:33:55Z",
    "modified": "2020-06-19T19:26:34Z",
    "name": "Note 2"
  },
  {
    "created": "2020-06-19T22:48:05Z",
    "modified": "2020-06-20T00:28:45Z",
    "name": "Note 3"
  }
]
```
---
