<template>
  <div>
    <p class="pl-2 border-b-2 border-gray-900 border-opacity-75">Notes</p>
    <!-- Collapsable list of notes -->
    <note-folder :folder="notes" :startExpanded="true"></note-folder>
  </div>
</template>

<script>
import { mapState } from "vuex";
import noteFolder from "./noteFolder.vue";

export default {
  components: {
    noteFolder,
  },
  data: function () {
    return {};
  },
  computed: {
    ...mapState(["notes"]),
  },
};
</script>
