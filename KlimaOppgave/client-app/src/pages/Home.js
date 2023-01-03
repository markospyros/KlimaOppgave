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

  const sortBasedOnTime = () => {
    for (let i = 0; i < posts.length - 1; i++) {
      if (posts[i].timeStamp < posts[i + 1].timeStamp) {
        const tmp = posts[i];
        posts[i] = posts[i + 1];
        posts[i + 1] = tmp;

        i = -1;
      }
    }

    return posts;
  };

  sortBasedOnTime();

  const addAnswer = (newAnswer, postId) => {
    const updatedPosts = posts.map((post) => {
      if (post.innleggId === postId) {
        post.svar.push(newAnswer);
      }
      return post;
    });
    setPosts(updatedPosts);
  };

  const deleteAnswer = (id) => {
    const updatedPosts = posts.map((post) => {
      post.svar = post.svar.filter((svar) => svar.svarId !== id);
      return post;
    });
    setPosts(updatedPosts);
  };

  const deletePost = (id) => {
    const updatedPosts = posts.filter((post) => post.innleggId !== id);

    setPosts(updatedPosts);
  };

  const formatPosts = posts.map((post) => {
    return (
      <div
        key={post.innleggId}
        className="card text-bg-light mb-3"
        style={{ maxWidth: "30rem" }}
      >
        <PostComponent
          id={post.innleggId}
          tittel={post.tittel}
          innhold={post.innhold}
          dato={post.dato}
          deletePost={deletePost}
        />
        <div>
          {post.svar.map((svar) => (
            <div key={svar.svarId}>
              <AnswerComponent
                svarId={svar.svarId}
                svarInnhold={svar.innhold}
                svarDato={svar.dato}
                deleteAnswer={deleteAnswer}
              />
            </div>
          ))}
        </div>
        <div>
          <AnswerInput innleggId={post.innleggId} addAnswer={addAnswer} />
        </div>
      </div>
    );
  });

  return <div>{formatPosts}</div>;
};

export default Home;
