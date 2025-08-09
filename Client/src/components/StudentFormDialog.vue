<template>
  <div class="dialog-overlay">
    <div class="dialog-content">
      <h3>{{ student ? 'Modifier élève' : 'Ajouter un élève' }}</h3>
      
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label>Prénom:</label>
          <input v-model="form.firstName" required>
        </div>
        <div class="form-group">
          <label>Nom:</label>
          <input v-model="form.lastName" required>
        </div>
        <div class="form-group">
          <label>Genre:</label>
          <select v-model="form.gender">
            <option value="M">Masculin</option>
            <option value="F">Féminin</option>
          </select>
        </div>

        <NoteManagement
          v-if="student"
          :student-id="form.id"
          :matieres="matieres"
          :initial-notes="form.notes"
          @notes-updated="handleNotesUpdated"
        />

        <div class="dialog-buttons">
          <button type="button" @click="$emit('close')">Annuler</button>
          <button type="submit">Enregistrer</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { addNote , updateNote } from '../api/noteApi';
import NoteManagement from './NoteManagement.vue';

export default {
  components: {
    NoteManagement
  },
  props: {
    student: Object,  
    classId: Number,
    matieres: { type: Array, required: true }
  },
  data() {
    return {
      form: {
        id: null,
        firstName: '',
        lastName: '',
        gender: 'M',
        notes: []
      },
    };
  },
  watch: {
    student: {
      immediate: true,
      handler(newVal) {
        if (newVal) {
          this.form = { ...newVal,
          notes : newVal.notes || []
          };
        } else {
          this.resetForm();
        }
      }
    }
  },
  methods: {
    resetForm() {
      this.form = {
        firstName: '',
        lastName: '',
        gender: 'M',
        notes : []
      };
    },
    submitForm() {
      this.$emit('save', this.form);
    },
    handleNotesUpdated(updateNotes) {
      this.form.notes = updateNotes;
    }
  }
};
</script>

<style scoped>
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}
.dialog-content {
  background: white;
  padding: 20px;
  border-radius: 8px;
  width: 400px;
}
.form-group {
  margin-bottom: 15px;
}
.form-group label {
  display: block;
  margin-bottom: 5px;
}
.form-group input, .form-group select {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}
.dialog-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
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