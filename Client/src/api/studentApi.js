const API_URL = 'http://localhost:5166/api/students';

export async function getStudentByClass(classId) {
    const res = await fetch (`${API_URL}/by-class/${classId}`) // to use the same API as controller Route
    return await res.json();
}

export async function addStudent(studentData) {
    const res = await fetch(`${API_URL}`, {
        method :'POST',
        headers :{'Content-Type' : 'application/json'},
        body: JSON.stringify(studentData)
    });
    return await res.json();
}

export async function updateStudent(studentData) {
    const res = await fetch(`${API_URL}/${studentData.id}`, {
        method :'PUT',
        headers: {'Content-Type' :'application/json'},
        body : JSON.stringify(studentData)
    });
      const text = await res.text();
      try {
          return JSON.parse(text); 
      } catch {
          return { message: text }; 
      }
}

export async function deleteStudent(id) {
    const res = await fetch (`${API_URL}/${id}`,{
        method :'DELETE'
    });
    if (!res.ok) {
    const error = await res.text(); 
    throw new Error(error || "Suppression échoué.");
  }

  try {
    return await res.json();
  } catch {
    return { success: true }; 
  }
}
