<template>
  <div class="dialog-overlay" >
    <div class="dialog-content">
        <h3>Assigner un professeur à une matière</h3>
            <form @submit.prevent="submitForm">
                <label>Professeur :</label>
                    <select v-model.number="form.teacherId" required>
                        <option disabled value="">-- Sélectionner un professeur --</option>
                        <option v-for="t in teachers" :key="t.id" :value="t.id">
                        {{ t.firstName }} {{ t.lastName }}
                        </option>
                    </select>

                    <label>Matière :</label>
                    <select v-model.number="form.matiereId" required>
                        <option disabled value="">-- Sélectionner une matière --</option>
                        <option v-for="m in matieres" :key="m.id" :value="m.id">
                        {{ m.nom }}
                        </option>
                    </select>
                    
                    <div class="dialog-action">
                        <button type="submit">Assigner</button>
                        <button type="button" @click="$emit('close')">Annuler</button>
                    </div>
            </form>
    </div>
  </div>
</template>

<script>
import { getAllMatieres } from '../api/matiereApi';
import { getAllTeachers } from '../api/teacherApi';

export default {
  data() {
    return {
        isLoading : false,
        teachers: [],
        matieres: [],
        form:{
            teacherId :null,
            matiereId :null
      }
    };
  },
  async mounted() {
    await this.loadData();
  },
  methods: {
    async loadData() {
        this.isLoading = true;
      try {
        const [teachersRes, matieresRes] = await Promise.all([
          getAllTeachers(),
          getAllMatieres()
        ]);
        this.teachers = teachersRes;
        this.matieres = matieresRes;
      } catch (error) {
        console.error("Erreur de chargement:", error);
      }
      this.isLoading=false;
    },
    async submitForm() {
         if (!this.form.teacherId || !this.form.matiereId) {
            alert("Veuillez sélectionner un professeur et une matière.");
            return;
            }     

      this.$emit('save', {
        teacherId: Number(this.form.teacherId),
        matiereId: Number(this.form.matiereId)
      });
    },
    closeDialog() {
      this.resetForm();
      this.$emit('close');
    },
    resetForm() {
      this.form = {
        teacherId: null,
        matiereId: null
      };
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
button {
  margin-right: 10px;
  padding: 8px 16px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
.dialog-action {
  display: flex;
  justify-content: flex-end; 
  gap: 12px; 
  margin-top: 20px; 
}

.dialog-action button[type="button"] {
  background: #f0f0f0;
  color: #333;
}

.dialog-action button[type="submit"] {
  background: #42b883;
  color: white;
  min-width: 100px; 
  padding: 10px 16px; 
}
</style>