<template>
    <div>
        <h2>Liste d'Elèves</h2>
        <h3>Classe No° 
            
            <select v-model="selectedClassId" @change="changeClass">
                <option v-for="cls in classes" :key="cls.id" :value="cls.id">
                   {{ cls.id }}
                </option>
            </select>

            - Niveau {{classNiveau}} </h3>

        <div class="action-buttons">
            <button @click="openAddStudentDialog">Ajouter un élève</button>
        </div>
    </div>

    <div class="prof-section">
        <h3> Professeur Principal : </h3>
        <p v-if="professeurPrincipal"> {{ professeurPrincipal.firstName }} {{ professeurPrincipal.lastName }}</p>
        <p v-else> Aucun professeur Principal</p>
    </div>

    <div class="student-list">
        <!-- liste d'élèves-->
        <ul v-if="students.length >0">
            <StudentItem
              v-for="student in students"
              :key="student.id"
              :student="student"
              @edit="openEditStudentDialog"
              @delete="confirmDelete"
              />
        </ul>
        <p v-else> Aucun élève dans cette classe.</p>

        <!-- ajout / modif élève pop up-->
         <StudentFormDialog
          v-if="showDialog"
          :student="editingStudent"
          :classId="classId"
          :matieres="matieres"
          @close="closeDialog"
          @save="handleSaveStudent"
          />
    </div>

            
    <div class="navigation-buttons">
        <button @click="goToTeachers"> Liste de Professeurs </button>
        <button @click="goToMatieres"> Liste de Matières </button>
    </div>

</template>

<script>
import StudentFormDialog from '../components/StudentFormDialog.vue';
import StudentItem from '../components/StudentItem.vue';
import { getStudentByClass, updateStudent, addStudent , deleteStudent} from '../api/studentApi';
import { getProfesseurPrincipal, getAllClasses } from '../api/classApi';
import { getAllMatieres } from '../api/matiereApi';

export default {
    components : { StudentItem, StudentFormDialog},
    props: {
        student: Object,
        classId: Number,
        matieres: { type: Array, required: false , default: ()=> [] }
        },
    data () {
        return {
            classes:[],
            classId : this.$route.params.id,
            selectedClassId : null,
            classNiveau : null,
            professeurPrincipal : null,
            niveau : null,
            students : [],
            matieres : [],
            showDialog : false,
            editingStudent : null
        };
    },
    async mounted() {
      await this.loadStudents();
      await this.loadProfesseurPrincipal();
      await this.loadClassInfo();
      await this.loadMatieres();
      await this.loadClasses();
    },
    watch: {
    '$route.params.id': {
      immediate: true,
        handler(newId) {
            if (newId) {
            this.classId = Number(newId);
            this.selectedClassId = Number(newId);
            this.reloadData();
            }
        }
        }
    },
    methods : {
        async loadMatieres(){
            this.matieres = await getAllMatieres();
        },
        async loadStudents(){
            this.students = await getStudentByClass(this.classId);
        },
        async loadClasses(){
            this.classes = await getAllClasses();
            this.selectedClassId = this.classId;
        },
        async loadProfesseurPrincipal(){
            this.professeurPrincipal = await getProfesseurPrincipal(this.classId);
        },
        async reloadData() {
            await this.loadClassInfo();
            await this.loadStudents();
            await this.loadProfesseurPrincipal();
            },
        async loadClassInfo() {
            try {
                const response = await fetch(`http://localhost:5166/api/classes/${this.selectedClassId}`);
                if (response.ok) {
                const classData = await response.json();
                this.classNiveau = classData.niveau || 'Inconnu'; 
                }
            } catch (error) {
            console.error('Erreur de chargement:', error);
            this.classNiveau = 'Erreur';
         }
        },
        changeClass() {
            if (!this.selectedClassId) return; 
            this.$router.push(`/class/${this.selectedClassId}`);
        },
        openAddStudentDialog() {
            this.editingStudent = null;
            this.showDialog = true;
        },
        openEditStudentDialog(student){
            this.editingStudent = {
                ...student ,
                notes: student.notes || [],
                matieres : student.matieres || []
            };
            this.showDialog = true;
        },
        closeDialog(){
            this.showDialog = false;
        },
        selectStudent(student){
            this.selectedStudent = student;
        },
        async confirmDelete(student){
            if(confirm(`Confirmez de supprimer ${student.firstName} ${student.lastName} ?`)) {
                await deleteStudent(student.id);
                await this.loadStudents();
            }
        },
        async handleSaveStudent(studentData) {
            try {
                if (studentData.id) {
                    await updateStudent(studentData);
                } else {
                    await addStudent({ ...studentData, classId: this.classId});
                }
            await this.loadStudents();
            this.closeDialog();
            } catch (error) {
                console.error("Erreur : ", error);
            }
        },
         goToTeachers() {
            this.$router.push('/teachers');
        }, 
        goToMatieres() {
            this.$router.push('/matieres');
        }
    }
};
</script>

<style scoped>
.action-buttons {
  margin-bottom: 20px;
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
button:disabled {
  background: #cccccc;
  cursor: not-allowed;
}
.professor-section {
  margin-top: 30px;
  padding: 15px;
  background-color: #f5f5f5;
  border-radius: 4px;
}
.navigation-buttons {
  margin-top: 100px;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.navigation-buttons button {
  padding: 8px 16px;
  background-color: #42b983;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
</style>