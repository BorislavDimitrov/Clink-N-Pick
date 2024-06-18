import ChangePassword from "../../components/ChangePassword";
import EditProfile from "../../components/EditProfile";

function UserSettings() {
  return (
    <div class="mt-10 bg-white w-full flex flex-col gap-5 px-3 md:px-16 lg:px-28 md:flex-row text-[#161931]">
      <aside class="py-10 w-full md:w-1/3 lg:w-1/4 md:ml-10 order-first md:order-none">
        <div class="sticky top-10 flex flex-col gap-2 p-4 text-sm border-r border-l border-blue-400 rounded-xl">
          <h2 class="pl-3 mb-4 text-2xl font-semibold flex items-center justify-center">
            Settings
          </h2>

          <a
            href="#publicProfile"
            class="flex items-center justify-center px-3 py-2.5 font-semibold hover:text-blue-900 hover:border hover:rounded-full hover:border-blue-500 cursor-pointer"
          >
            Public Profile
          </a>
          <a
            href="#changePassword"
            class="flex items-center justify-center px-3 py-2.5 font-semibold hover:text-blue-900 hover:border hover:rounded-full hover:border-blue-500  cursor-pointer"
          >
            Change Password
          </a>
        </div>
      </aside>
      <main class="w-full min-h-screen py-1 md:w-2/3 lg:w-3/4">
        <EditProfile />
        <ChangePassword />
      </main>
    </div>
  );
}

export default UserSettings;
