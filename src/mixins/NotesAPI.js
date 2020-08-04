import axios from "axios";

function UrlJoin(...args) {
    return args.map(x => x.replace(/^\/|\/$/gm, '')).join("/")
}

// Get the notes data
const NotesAPI = {
    methods: {
        getNotes() {
            return axios.get(UrlJoin(this.$store.state.endpoint, "/notes"));
        },
        updateFolder(folderId, folderObject) {
            return axios.put(UrlJoin(this.$store.state.endpoint, `/Folders/${folderId}`), folderObject)
        },
        updateNote(noteId, noteObject) {
            return axios.put(UrlJoin(this.$store.state.endpoint, `/Notes/${noteId}`), noteObject)

        }
    }
}

export default NotesAPI