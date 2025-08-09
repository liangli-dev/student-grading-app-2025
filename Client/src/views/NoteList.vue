<template>
  <table v-if="notes.length > 0" class="note-table">
    <thead>
      <tr>
        <th>Matière</th>
        <th>Note</th>
        <th>Action</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(note, index) in notes" :key="index">
        <td>{{ findMatiereName(note.matiereId) }}</td>
        <td>{{ note.valeur }}</td>
        <td class="actions">
          <button type="button" @click="$emit('edit', note.matiereId)">
            Modifier
          </button>
          <button type="button" @click="$emit('remove', note.id)">
            Supprimer
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  props: {
    notes: {
      type: Array,
      required: true
    },
    matieres: {
      type: Array,
      required: true
    }
  },
  methods: {
    findMatiereName(matiereId) {
      if (!Array.isArray(this.matieres)) return 'Inconnue';
      const matiere = this.matieres.find(m => m.id === matiereId);
      return matiere ? matiere.nom : 'Inconnue';
    }
  }
};
</script>

<style scoped>
.note-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 10px;
  box-sizing: border-box;
}
.note-table th, .note-table td {
  padding: 8px;
  border: 1px solid #ddd;
  text-align: left;
}
.note-table th {
  background-color: #f5f5f5;
}
.actions {
  display: flex;
  gap: 5px;
}
.actions button {
  flex: 1; 
  min-width: 80px; 
  padding: 6px 10px;
}
button {
  margin-right: 10px;
  padding: 8px 16px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>