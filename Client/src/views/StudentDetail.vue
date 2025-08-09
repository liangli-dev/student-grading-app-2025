<template>
    <div>
        <h2>Détails de l'élève {{ studentId }}</h2>
        <ul>
            <li v-for="note in notes" :key="note.id">
                {{ note.Matiere.Nom }} : {{ note.Valeur }}
            </li>
        </ul>
        <router-link :to="`/student/${studentId}/add-note`"> Ajouter une note</router-link>
    </div>
</template>

<script>
import { getNoteByStudent } from '../api/noteApi';

export default {
    data(){
        return {
            studentId : this.$route.parmas.id,
            notes : []
        };
    },
    async mounted() {
        this.notes = await getNoteByStudent(this.studentId);
    }
};
</script>