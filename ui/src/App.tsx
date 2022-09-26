import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./shell/Layout";
import NoPage from "./shell/NoPage";
import Dashboard from "./dashboard/Dashboard";
import WordPress from "./word-press/WordPress";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index={true} element={<Dashboard />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="word-press" element={<WordPress />} />
          <Route path="*" element={<NoPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
