# Documentation
The api is accessible through the `http://localhost:5000/api/` route.\
When using post routes don't forget to specify the `content-type` as `application/json` in the request headers.

## Setup
[Install python 3.8.2 or higher.](https://www.python.org/downloads/) \
Install requirements
```bash
pip install -r requirements.txt
```
Run `app.py`
```bash
python app.py
```

## Endpoints
#### URL: `/api/notes`
#### Method: `GET`
#### Description:
Returns a list of all note data.
#### Sample response:
Request: `.get("http://localhost:5000/api/notes")`
```json
{
  "id": "root",
  "name": "root",
  "folders": [
    {
      "id": "root",
      "name": "root",
      "folders": [],
      "notes": [
        {
          "id": "21131255",
          "name": "New Note",
          "content": "<h1>Note content</h1>",
          "tags": ["#tag"],
          "links": []
        }
      ]
    }
  ],
  "notes": [
    {
      "id": "12353566",
      "name": "New Note",
      "content": "<h1>Note content</h1>",
      "tags": ["#tag"],
      "links": ["21131255"]
    }
  ]
}
```
#### URL: `/api/notes`
#### Method: `POST`
#### Description:
Saves the notes data.