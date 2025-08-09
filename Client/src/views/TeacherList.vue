<template>
    <div>
        <h2> Liste des Professeurs</h2>

        <div class="action-buttons">
            <button @click="openAddDialog">Ajouter un nouveau professeur</button>
        </div>

        <!-- liste des profs-->
        <ul v-if="teachers.length >0">
            <li class="teacher-item" v-for="t in teachers" :key="t.id">
                <div class="=teacher-info">
                   ID: {{ t.id }} . {{ t.firstName }} {{ t.lastName }} - {{ t.gender }}
                </div>
                <div class="action-buttons">
                    <button @click="editTeacher(t)">Modifier</button>
                    <button @click="deleteTeacherConfirm(t)">Supprimer</button>
                </div>
            </li>
        </ul>
        <p v-else> Aucun professeur trouvé.</p>

        <!-- ajout / modif prof pop up-->
         <TeacherFormDialog
          v-if="showDialog"
          :teacher="editingTeacher"
          @close="showDialog = false"
          @save="handleSaveTeacher"
          />
    </div>

    <hr class="section-divider">

    <div>
    <h2>Liste des Professeurs par Matière</h2>

    <div v-for="matiere in matieres" :key="matiere.matiereId" class="matiere-block">
      <h3>{{ matiere.matiereNom }}</h3>

      <ul v-if="matiere.teachers.length > 0">
        <li v-for="t in matiere.teachers" :key="t.id">
          {{ t.firstName }} {{ t.lastName }} - {{ t.gender }}
        </li>
      </ul>
      <p v-else>Aucun professeur pour cette matière</p>
    </div>
  </div>
</template>

<script>

import TeacherFormDialog from '../components/TeacherFormDialog.vue';
import { getAllTeachers, addTeacher, updateTeacher, deleteTeacher ,getTeacherGroupedByMatiere} from '../api/teacherApi';

export default {
    components :{ TeacherFormDialog },
    data(){
        return {
            teachers: [],
            showDialog : false,
            editingTeacher :null,
            matieres : []
        };
    },
    async mounted() {
        await this.loadTeachers();
        this.matieres = await getTeacherGroupedByMatiere();
    },
    methods : {
        async loadTeachers(){
            this.teachers = await getAllTeachers();
        },
        openAddDialog(){
            this.editingTeacher = {};
            this.showDialog = true;
        },
        editTeacher(teacher) {
            this.editingTeacher = {...teacher};
            this.showDialog = true;
        },
        async handleSaveTeacher(teacherData) {
           try
            { if(teacherData.id) {
                await updateTeacher(teacherData);
            } else {
                await addTeacher(teacherData);
            }
            await this.loadTeachers();
            this.showDialog = false;}
            catch (error){
                console.error("Erreur suppression : ", error);
            }
        },
        async deleteTeacherConfirm(teacher) {
            if(confirm(`Supprimer ${teacher.firstName} ${teacher.lastName} ?`)) {
                await deleteTeacher(teacher.id);
                await this.loadTeachers();
            }
        }
    }
}
</script>

<style>
/* .teacher-list {
  list-style: none;
  padding: 0;
} */

.teacher-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #eee;
  padding: 10px 0;
}

.teacher-info {
  font-size: 16px;
}

.action-buttons {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.action-buttons button {
  padding: 6px 12px;
  background-color: #42b983;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>