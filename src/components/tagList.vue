<template>
    <div>
        <div>
            <div v-for="tag in tags" :key="tag" class="h-16 w-full inline mx-2" @click="tester()">{{tag}}</div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        tags: Array
    },
    methods: {
        tester: function() {
            console.log("dummy")
        }
    }
}
</script>