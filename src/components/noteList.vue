<template>
  <div>
    <div class="h-12 text-2xl p-4 mb-4">Note List:</div>
    <!-- Collapsable list of notes -->
    <note-folder :folder="notes"></note-folder>
  </div>
</template>

<script>
import { mapState } from "vuex";
import noteFolder from "./noteFolder.vue";

export default {
  components: {
    noteFolder
  },
  data: function() {
    return {};
  },
  computed: {
    ...mapState(["test", "notes"])
  }
};
</script>
