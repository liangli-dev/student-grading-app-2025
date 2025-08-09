<template>
  <div class="notes-section">
    <h4>Gestion des Notes</h4>
    
    <div class="note-editor">
      <select 
        v-model="selectedMatiere" 
        @change="loadNoteForMatiere"
      >
        <option value="">-- Sélectionner une matière --</option>
        <option 
          v-for="matiere in matieres" 
          :key="matiere.id" 
          :value="matiere.id"
        >
          {{ matiere.nom }}
        </option>
      </select>

      <input 
        class="note-input"
        type="number"
        v-model.number="currentNote.valeur"
        min="0"
        max="20"
        placeholder="Note (0-20)"
        :disabled="!selectedMatiere"
      >

      <button 
        type="button" 
        @click="saveNoteForMatiere"
        :disabled="!selectedMatiere || currentNote.valeur === null"
      >
        {{ existingNoteIndex >= 0 ? 'Mettre à jour' : 'Ajouter' }}
      </button>
    </div>

    <NoteList 
      :notes="notes"
      :matieres="matieres"
      @edit="editNote"
      @remove="removeNote"
    />
  </div>
</template>

<script>
import { addNote, updateNote, deleteNote, getNoteByStudent } from '../api/noteApi';
import NoteList from '../views/NoteList.vue';

export default {
  components: {
    NoteList
  },
  props: {
    studentId: { type: Number, required: true },
    matieres: { type: Array, required: true },
  },
  data() {
    return {
      notes: [],
      selectedMatiere: null,
      currentNote: { valeur: null },
      existingNoteIndex: -1
    };
  },
    async mounted() {
        await this.loadNotes();
    },
  methods: {
    async loadNotes() {
        this.notes = await getNoteByStudent(this.studentId);
        this.$emit('notes-updated', this.notes);
        },
    loadNoteForMatiere() {
      this.existingNoteIndex = this.notes.findIndex(
        note => note.matiereId === this.selectedMatiere
      );
      
      if (this.existingNoteIndex >= 0) {
        this.currentNote = { ...this.notes[this.existingNoteIndex] };
      } else {
        this.currentNote = { valeur: null };
      }
    },
    async saveNoteForMatiere() {
      if (!this.selectedMatiere || this.currentNote.valeur === null) return;
     
      if (this.existingNoteIndex >= 0) {
        const existing = this.notes[this.existingNoteIndex];
        const noteData = {
          id: existing.id,
          studentId: this.studentId,
          matiereId: this.selectedMatiere,
          valeur: this.currentNote.valeur
        };
        await updateNote(noteData.id, noteData);
      } else {
        const newNoteData = {
          studentId: this.studentId,
          matiereId: this.selectedMatiere,
          valeur: this.currentNote.valeur
        };
        const newNote = await addNote(newNoteData);
        this.notes.push(newNote);
      }
      await this.loadNotes(); 
      this.selectedMatiere = null;
      this.currentNote = { valeur: null };
    },
    editNote(matiereId) {
      this.selectedMatiere = matiereId;
      this.loadNoteForMatiere();
    },
    async removeNote(noteId) {
      await deleteNote(noteId);
      await this.loadNotes();
    }
  }
};
</script>

<style scoped>
.notes-section {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid #eee;
}
.note-editor {
  display: flex;
  gap: 10px;
  margin-bottom: 15px;
}
.note-input {
  width: 120px;
}
</style>