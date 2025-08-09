const API_URL = 'http://localhost:5166/api/matieres'; 

export async function getAllMatieres() {
  const res = await fetch(API_URL);
  return await res.json();
}

export async function getSubjectsByTeacher(teacherId) {
    const res = await fetch(`${API_URL}/by-teacher/${teacherId}`);
    return await res.json();
}

export async function assignSubjectToTeacher(teacherId, matiereId) {
     console.log("Sending to API:", {
        teacherId: Number(teacherId),
        matiereId: Number(matiereId)
    });
    const res = await fetch(`${API_URL}/assign-to-teacher`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ teacherId : Number(teacherId), matiereId : Number(matiereId) ,id: 0 })
    });
     if (!res.ok) {
        const errorText = await res.text(); 
        throw new Error(`Échec de l'assignation: ${errorText}`);
    }
    return await res.json();
}

export async function unassignTeacherFromSubject(teacherId, matiereId) {
    const res = await fetch(`${API_URL}/unassign/${teacherId}/${matiereId}`, {
        method:'DELETE'
    });
    if(!res.ok) throw new Error('Erreur lors de la suppression.');
    return await res.json();    
}
