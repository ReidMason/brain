<template>
  <div>
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
