<template>
  <div id="app">
    <div class="flex fexl-row">
      <sidebar></sidebar>
      <!-- Main screen content -->
      <div class="flex flex-col h-screen w-full bg-gray-500">
        <!-- Editor section -->
        <div class="flex h-full">
          <editor
            v-for="(note, index) in this.$store.getters.selectedNotes"
            :key="index + note.id"
            :index="index"
            :immutableNote="note"
          />
        </div>

        <!-- Details footer -->
        <div class="bg-gray-500 w-full px-2">
          <div v-if="focusedNote" class="flex flex-row-reverse">
            <span class="ml-4">Words: {{ focusedNote.content.split(" ").length }}</span>
            <span>Characters: {{ focusedNote.content.length }}</span>
          </div>
        </div>
      </div>

      <!-- Element for dragging notes and folders -->
      <div
        id="dragElement"
        class="bg-gray-500 p-1 rounded border-2 border-gray-800"
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
    ...mapState(["tags", "searchPhrase", "focusedNote"])
  },
  mounted() {
    document.addEventListener(
      "drag",
      function(ev) {
        var element = document.getElementById("dragElement");
        element.style.left = ev.clientX + 10 + "px";
        element.style.top = ev.clientY + 10 + "px";
      },
      false
    );
  }
};
</script>
