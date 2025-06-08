import { useEffect, useState } from "react";
import Navbar from "../components/Navbar";
import { UserRoles, type User } from "../models/user";
import { getUsers, updateUserRole } from "../api/users";
import Loader from "../components/Loader";
import ErrorPage from "../components/ErrorPage";
import ErrorDialog from "../components/dialogs/ErrorDialog";

const AdminManagement = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const roles = [{roleValue: UserRoles.ADMIN, role: "Admin"}, {roleValue: UserRoles.TEAMLEAD, role: "Team lead"}, {roleValue: UserRoles.TODO, role: "Todo User"}];
  const [errorPageMessage, setErrorPageMessage] = useState("");
  const [errorDialogMessage, setErrorDialogMessage] = useState("");

  useEffect(() => {
    setLoading(true);
    const fetchUsers = async () => {
      try {
        const data = await getUsers();
        setUsers(data);
      } catch (error) {
        setErrorPageMessage(
          error instanceof Error ? error.message : "An unknown error occurred"
        );
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  const handleRoleChange = async (id: number, newRole: string) => {
    try {
      await updateUserRole(id, newRole);
      setUsers((prev) =>
        prev.map((user) => (user.id === id ? { ...user, roleName: newRole } : user))
      );
    } catch (error) {
      setErrorDialogMessage(
        error instanceof Error ? error.message : "An unknown error occurred"
      );
    }
  };

  return (
    <>
      <Navbar isAdminPage />
      {loading ? (
        <Loader />
      ) : errorPageMessage ? (
        <ErrorPage
          errorMessage={errorPageMessage}
          errorTitle="An error has occurred"
        />
      ) : (
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
                {users.sort((a, b) => a.username.localeCompare(b.username)).map((user) => (
                  <tr key={user.id}>
                    <td className="px-6 py-4 whitespace-nowrap text-lg text-gray-900">
                      {user.username}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap text-gray-700">
                      {user.email}
                    </td>
                    <td className="px-6 py-4 whitespace-nowrap">
                      <select
                        className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-2 focus:ring-blue-400 focus:border-blue-400 outline-none"
                        value={user.roleName}
                        onChange={(e) =>
                          handleRoleChange(user.id, e.target.value)
                        }
                      >
                        {roles.map((role) => (
                          <option key={role.roleValue} value={role.roleValue}>
                            {role.role}
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
      )}
      <ErrorDialog
        errorMessage={errorDialogMessage}
        isOpen={errorDialogMessage !== ""}
        onClose={() => setErrorDialogMessage("")}
      />
    </>
  );
};

export default AdminManagement;
