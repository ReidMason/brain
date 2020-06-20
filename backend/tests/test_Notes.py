import os
from unittest import TestCase

from models.Notes import Notes

NOTES_PATH = "../../data/test/"


class TestNotes(TestCase):
    notes = Notes(NOTES_PATH)

    @classmethod
    def tearDownClass(cls) -> None:
        os.rmdir(NOTES_PATH)

    def tearDown(self) -> None:
        for file in os.listdir(NOTES_PATH):
            os.remove(os.path.join(NOTES_PATH, file))

    def test_save_note_creation(self):
        note_content = "<h1>This is a new note</h1>"
        note_name = "Note 1"
        note_data = {
            "target": None,
            "note"  : {
                "contents": note_content,
                "name"    : note_name
            }
        }

        self.notes.save_note(note_data.get('target'), note_data.get('note'))
        note_path = os.path.join(NOTES_PATH, note_name + ".html")

        # Check file was created
        self.assertTrue(os.path.exists(note_path))

        # Check file content is correct
        with open(note_path, 'r') as f:
            self.assertEqual(f.read(), note_content)

    def test_save_note_edit(self):
        note_content = "<h1>This is a new note</h1>"
        note_name = "Note 1"
        note_data = {
            "target": None,
            "note"  : {
                "contents": note_content,
                "name"    : note_name
            }
        }

        self.notes.save_note(note_data.get('target'), note_data.get('note'))
        note_path = os.path.join(NOTES_PATH, note_name + ".html")

        new_contents = "<h1>This is a changed note</h1>"
        note_data['note']['contents'] = new_contents

        # Save new file content
        self.notes.save_note(note_name, note_data.get('note'))

        # Check file content is correct
        with open(note_path, 'r') as f:
            self.assertEqual(f.read(), new_contents)

    def test_save_note_rename(self):
        note_content = "<h1>This is a new note</h1>"
        note_name = "Note 1"
        note_data = {
            "target": None,
            "note"  : {
                "contents": note_content,
                "name"    : note_name
            }
        }

        self.notes.save_note(note_data.get('target'), note_data.get('note'))
        modified = self.notes.notes[0].modified

        new_name = "Note 2"
        note_data['note']['name'] = new_name

        # Rename the note
        self.notes.save_note(note_name, note_data.get('note'))

        note_path = os.path.join(NOTES_PATH, note_name + ".html")
        new_note_path = os.path.join(NOTES_PATH, new_name + ".html")

        # Check file name
        self.assertTrue(os.path.exists(new_note_path))
        self.assertFalse(os.path.exists(note_path))
        self.assertNotEqual(modified, self.notes.notes[0].modified)

    def test_save_note_manually_removed(self):
        note_content = "<h1>This is a new note</h1>"
        note_name = "Note 1"
        note_data = {
            "target": None,
            "note"  : {
                "contents": note_content,
                "name"    : note_name
            }
        }

        self.notes.save_note(note_data.get('target'), note_data.get('note'))

        # Delete the note
        note_path = os.path.join(NOTES_PATH, note_name + ".html")
        os.remove(note_path)

        # Edit note contents
        new_content = "<h1>This is a changed note</h1>"
        note_data['target'] = note_name
        note_data['note']['content'] = new_content
        note_path = os.path.join(NOTES_PATH, note_name + ".html")

        self.notes.save_note(note_data.get('target'), note_data.get('note'))

        self.assertTrue(os.path.exists(note_path))

    def test_save_note_update_references(self):
        # Create Note 1 to be renamed
        note_content = "<h1>This is a new note[[Note 1]] [[Note 1]] </h1>"
        note_name = "Note 1"
        note_data = {
            "target": None,
            "note"  : {
                "contents": note_content,
                "name"    : note_name
            }
        }

        self.notes.save_note(note_data.get('target'), note_data.get('note'))

        # Create Note 2 that links to Note 1
        note_2_content = '<h1>This is a second new note[[Note 1]] [[Note 1]] </h1>'
        note_data['note']['contents'] = note_2_content
        note_data['note']['name'] = 'Note 2'

        self.notes.save_note(note_data.get('target'), note_data.get('note'))

        # Rename Note 1
        note_data['target'] = 'Note 1'
        note_data['note']['name'] = 'Renamed 1'
        note_data['note']['contents'] = "<h1>This is a renamed note[[Note 1]] [[Note 1]] </h1>"

        self.notes.save_note(note_data.get('target'), note_data.get('note'))

        # Test the contents
        expected_note_2_content = '<h1>This is a second new note[[Renamed 1]] [[Renamed 1]] </h1>'
        expected_note_content = "<h1>This is a renamed note[[Renamed 1]] [[Renamed 1]] </h1>"

        self.assertEqual(expected_note_content, self.notes.get_note('Renamed 1').get_note_contents())
        self.assertEqual(expected_note_2_content, self.notes.get_note('Note 2').get_note_contents())


if __name__ == '__main__':
    testNotes = TestNotes()
    testNotes.run()
