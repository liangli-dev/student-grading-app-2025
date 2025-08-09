<template>
  <div class="dialog-overlay">
    <div class="dialog-content">
      <h3>{{ teacher.id ? 'Modifier le prof' : 'Ajouter un professeur' }}</h3>
      
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
            <div class="action">
                <button type="submit">Enregistrer</button>
                <button type="button" @click="$emit('close')">Annuler</button>
            </div>
        </form>
    </div>
   </div>
</template>

<script>
export default {
    props :{
        teacher : {
            type : Object,
        default: () => ({})
        }
    },
    data() {
        return {
            form: {
                id : null,
                firstName:'',
                lastName:'',
                gender: 'M'
            }
        };
    },
    watch:{
        teacher: {
        immediate : true,
        handler(newVal) {
            if(newVal) {
             this.form = {...this.form , ...newVal}
            } else {
                this.resetForm();
            }
          }
        }
    },
    methods : {
        submitForm(){
            this.$emit('save', this.form);
        }
    }
}
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
</style>