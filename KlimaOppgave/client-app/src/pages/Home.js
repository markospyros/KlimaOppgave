import axios from "axios";
import React, { useEffect, useState } from "react";
import AnswerInput from "../components/AnswerInput";
import AnswerComponent from "../components/AnswerComponent";
import PostComponent from "../components/PostComponent";

const Home = () => {
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    axios.get("/hentinnlegg").then((response) => setPosts(response.data));
  }, []);

  const formatPosts = posts.map((post) => {
    return (
      <div
        key={post.innleggId}
        className="card text-bg-light mb-3"
        style={{ maxWidth: "30rem" }}
      >
        <PostComponent innhold={post.innhold} dato={post.dato} />
        <div>
          {post.svar.map((svar) => (
            <div key={svar.svarId}>
              <AnswerComponent
                svarInnhold={svar.innhold}
                svarDato={svar.dato}
              />
            </div>
          ))}
        </div>
        <div>
          <AnswerInput innleggId={post.innleggId} />
        </div>
      </div>
    );
  });

  return <div>{formatPosts}</div>;
};

export default Home;
