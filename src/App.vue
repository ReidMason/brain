<template>
  <div id="app" class="text-nord-4">
    <div class="flex fexl-row">
      <sidebar></sidebar>
      <!-- Main screen content -->
      <div class="h-screen w-full bg-nord-1">
        <!-- Editor section -->
        <div class="h-full flex">
          <editor
            v-for="(note, index) in $store.getters.selectedNotes"
            :key="index + note.id"
            :index="index"
            :immutableNote="note"
          />
        </div>

        <!-- Details footer -->
        <div class="bg-nord-0 px-2">
          <div v-if="focusedNote" class="flex flex-row-reverse">
            <span class="ml-4">Words: {{ focusedNote.content.split(" ").length }}</span>
            <span>Characters: {{ focusedNote.content.length }}</span>
          </div>
          <!-- TODO: Find a place for the tags and implement them properly -->
          <!-- <div>
          <div>{{ searchPhrase }}</div>
          <div v-for="tag in tags" :key="tag" >{{ tag }}</div>
        </div>-->
        </div>
      </div>

      <!-- Element for dragging notes and folders -->
      <div
        id="dragElement"
        class="bg-nord-1 p-1 rounded border-2 border-nord-3"
        style="position: absolute; white-space: nowrap;"
        v-if="$store.getters.movingElement"
      >
        <p>{{ $store.getters.movingElement.name }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import editor from "./components/editor";
import { mapState } from "vuex";
import sidebar from "./components/sidebar";
import axios from "axios";

export default {
  name: "App",
  components: {
    editor,
    sidebar
  },
  data: function() {
    return {
      items: [],
      selectedNote: null
    };
  },
  computed: {
    ...mapState(["tags", "searchPhrase", "focusedNote", "endpoint"])
  },
  created() {
    // Get the notes data
    axios.get(`${this.endpoint}/notes`).then(response => {
      this.$store.state.notes = response.data;
    });
  },
  mounted() {
    document.addEventListener(
      "drag",
      function(ev) {
        var element = document.getElementById("dragElement");
        element.style.left = ev.clientX + 12 + "px";
        element.style.top = ev.clientY + -20 + "px";
      },
      false
    );
  }
};
</script>

<style>
  input:focus,
  select:focus,
  textarea:focus,
  button:focus {
      outline: none;
  }
</style>