<template>
    <form @submit.prevent = "submitNote">
        <div>
            <label>Matière Id : </label>
            <input v-model=" note.MatiereId" type = "number" required />
        </div>
        <div>
            <label>Valeur : </label>
            <input v-model.number="note.Valeur" type="number" min="0" max="20" required />
        </div>
        <button type="submit">Ajouter Note</button>
    </form>
</template>

<script>
export default {
    props: ['studentId'],
    data() {
        return {
            note : {
                StudentId : this.StudentId,
                MatiereId :'',
                Valeur :''
            }
        };
    },
    methods : {
        async submitNote(){
                const res = await fetch('http://localhost:5166/api/notes', {
                    method : 'POST',
                    header : {'Content-Type' : 'application/json'},
                    body : JSON.stringify(this.note)
                });
                const result = await res.json();
                alert("Note ajoutée.");
                this.note.MatiereId = '';
                this.note.Valeur = '';
        }
    }
};
</script>