import axios from "axios";

function UrlJoin(...args) {
    return args.map(x => x.replace(/^\/|\/$/gm, '')).join("/")
}

// Get the notes data
const NotesAPI = {
    methods: {
        getNotes() {
            return axios.get(UrlJoin(this.$store.state.endpoint, "/notes"));
        }
    }
}

export default NotesAPI