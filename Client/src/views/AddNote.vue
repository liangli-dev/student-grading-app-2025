<template>
    <div>
        <h2> Ajouter une note pour l'élève {{ studentId }}</h2>
        <NoteForm :studentId="studentId" />
    </div>
</template>

<script>
import NoteForm from '../components/NoteForm.vue';

export default {
    Components : { NoteForm},
    data() {
        return {
            studentId : this.$route.params.id
        };
    }
};
</script>