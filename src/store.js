import axios from "axios";
import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        endpoint: "https://localhost:44339/api/",
        tags: [],
        searchPhrase: "",
        selectedNotes: [],
        focusedNote: null,
        movingElement: null,
        notes: {},
    },
    actions: {
        save(state) {
            axios.post(`${state.state.endpoint}/notes`, state.state.notes)
        },
    },
    mutations: {
        setTags(state, tags) {
            state.tags = tags;
        },
        setSearchPhrase(state, phrase) {
            state.searchPhrase = phrase;
        },
        addToSelectedNotes(state, note) {
            state.selectedNotes.push(note)
            state.focusedNote = note
        },
        setFocusedNote(state, note) {
            state.focusedNote = note;
        },
        setMovingElement(state, note) {
            state.movingElement = note;
        },
    },
    getters: {
        notes: (state) => {
            return state.notes;
        },
        selectedNotes: (state) => {
            return state.selectedNotes;
        },
        movingElement: (state) => {
            return state.movingElement;
        }
    }
});

export default store;