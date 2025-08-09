<template>
  <div>
    <h2>Liste des Matières</h2>

    <div class="action-buttons">
      <button @click="openAssignDialog">Assigner un professeur</button>
    </div>

    <ul v-if="subjects.length > 0">
      <li class="matiere-item" v-for="matiere in subjects" :key="matiere.id">
        <div class="matiere-info">
          <strong>{{ matiere.nom }} :</strong>
          <div class="teachers">
            <div v-if="matiere.teacherMatieres && matiere.teacherMatieres.length">
              <div
                class="teacher-row"
                v-for="tm in matiere.teacherMatieres"
                :key="tm.teacherId"
              >
                <span>
                  {{ tm.teacher?.firstName }} {{ tm.teacher?.lastName }}
                </span>
                <button
                  class="action-buttons"
                  @click="removeTeacherFromSubject(tm.teacherId, matiere.id)"
                >
                  Supprimer
                </button>
              </div>
            </div>
            <div v-else class="no-teacher">
              Aucun professeur assigné
            </div>
          </div>
        </div>
      </li>
    </ul>

    <p v-else>Aucun matière trouvée.</p>

    <MatiereFormDialog
      v-if="showDialog"
      @close="closeDialog"
      @save="handleAssign"
    />
  </div>
</template>


<script>
import { getAllMatieres, assignSubjectToTeacher,unassignTeacherFromSubject} from '../api/matiereApi';
import MatiereFormDialog from '../components/MatiereFormDialog.vue';

export default {
    components: {
        MatiereFormDialog
    },
    data(){
        return{
            subjects: [],
            showDialog :false
        };
    },
    async mounted() {
        await this.loadSubjects();
    },
    methods : {
        async loadSubjects(){
            this.subjects = await getAllMatieres();
        },
        openAssignDialog(){
            this.showDialog = true;
        },
        closeDialog(){
           this.showDialog = false; 
        },
        async handleAssign({teacherId ,matiereId}) {
            if (!teacherId || !matiereId) {
                console.error("ID manquant");
                return;
            }
            console.log("parametres : ", teacherId, matiereId);
            await assignSubjectToTeacher(teacherId, matiereId);
            await this.loadSubjects();
            this.showDialog = false;
        },
        async removeTeacherFromSubject(teacherId,matiereId) {
            if(confirm("Confirmez la supression du professeur de cette matière ?")) {
                try {
                    await unassignTeacherFromSubject(teacherId, matiereId);
                    await this.loadSubjects();
                }catch(error) 
                {
                    console.error("Erreur suppression : ", error);
                }
            };
        }
    }
}

</script>

<style>
.matiere-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #eee;
  padding: 10px 0;
}

.matiere-info {
  font-size: 16px;
}

.action-buttons {
  display: flex;
  justify-content: center;
  gap: 10px;
}

.teacher-row {
  display: flex;
  align-items: center;
  margin-bottom: 10px; 
}

.action-buttons button {
  padding: 6px 12px;
  background-color: #42b983;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
.item-actions button {
  margin-left: 10px;
  padding: 5px 10px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 3px;
}
</style>