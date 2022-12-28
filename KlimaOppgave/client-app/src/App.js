import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AskQuestion from "./pages/AskQuestion";
import Home from "./pages/Home";
import SharedLayout from "./pages/SharedLayout";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<SharedLayout />}>
          <Route index element={<Home />} />
          <Route path="askquestion" element={<AskQuestion />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
};

export default App;
