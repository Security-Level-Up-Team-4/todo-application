import { useState } from "react";
import Navbar from "../components/Navbar";

const mockUsers = [
  { id: 1, name: "User 1", email: "user1@bbd.co.za", role: "User" },
  { id: 2, name: "User 2", email: "user2@bbd.co.za", role: "Admin" },
  { id: 3, name: "User 3", email: "user3@bbd.co.za", role: "User" },
];

const roles = ["User", "Admin", "Team member"];

const AdminManagement = () => {
  const [users, setUsers] = useState(mockUsers);

  const handleRoleChange = (id: number, newRole: string) => {
    setUsers((prev) =>
      prev.map((user) => (user.id === id ? { ...user, role: newRole } : user))
    );
  };

  return (
    <>
      <Navbar />
      <main className="m-auto bg-white shadow-lg rounded-lg p-8 w-full max-w-3xl">
        <h1 className="text-3xl font-bold text-center text-gray-800 mb-8">
          Admin Management
        </h1>
        <section className="overflow-x-auto">
          <table className="min-w-full sectionide-y sectionide-gray-200">
            <thead className="bg-gray-100">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Name
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Email
                </th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Role
                </th>
              </tr>
            </thead>
            <tbody className="bg-white sectionide-y sectionide-gray-200">
              {users.map((user) => (
                <tr key={user.id}>
                  <td className="px-6 py-4 whitespace-nowrap text-lg text-gray-900">
                    {user.name}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-gray-700">
                    {user.email}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <select
                      className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-2 focus:ring-blue-400 focus:border-blue-400 outline-none"
                      value={user.role}
                      onChange={(e) =>
                        handleRoleChange(user.id, e.target.value)
                      }
                    >
                      {roles.map((role) => (
                        <option key={role} value={role}>
                          {role}
                        </option>
                      ))}
                    </select>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </section>
      </main>
    </>
  );
};

export default AdminManagement;
